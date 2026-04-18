using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using XMS.Core.Abstractions.EventBus;
using XMS.EventBus.Abstractions;
using XMS.Integration.Abstractions;

namespace XMS.Infrastructure.EventBus;

internal class RabbitMqIntegrationConsumer(
    IServiceProvider serviceProvider,
    IConnectionFactory connectionFactory,
    IEventNamingService eventNaming,
    ILogger<RabbitMqIntegrationConsumer> logger,
    List<Type> handlers) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using var connection = await connectionFactory.CreateConnectionAsync(ct);

        string DeadLetterExchange = $"{eventNaming.DeadLetterName}.exchange";
        string DeadLetterQueue = $"{eventNaming.DeadLetterName}.queue";

        using var deadLetterChannel = await connection.CreateChannelAsync(cancellationToken: ct);
        await deadLetterChannel.ExchangeDeclareAsync(DeadLetterExchange, ExchangeType.Fanout, durable: true, cancellationToken: ct);
        await deadLetterChannel.QueueDeclareAsync(DeadLetterQueue, durable: true, exclusive: false, autoDelete: false, cancellationToken: ct);
        await deadLetterChannel.QueueBindAsync(DeadLetterQueue, DeadLetterExchange, string.Empty, cancellationToken: ct);

        foreach (var handlerType in handlers)
        {
            var channel = await connection.CreateChannelAsync(cancellationToken: ct);

            var currentHandlerType = handlerType;
            var eventType = currentHandlerType.GetGenericArguments()[0];
            var eventName = eventNaming.GetEventName(eventType);

            var args = new Dictionary<string, object?> { { "x-dead-letter-exchange", DeadLetterExchange } };
            await channel.ExchangeDeclareAsync(exchange: eventName, ExchangeType.Fanout, durable: true, cancellationToken: ct);
            await channel.QueueDeclareAsync(queue: eventName, durable: true, exclusive: false, autoDelete: false, arguments: args, cancellationToken: ct);
            await channel.QueueBindAsync(queue: eventName, exchange: eventName, routingKey: string.Empty, cancellationToken: ct);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                using var scope = serviceProvider.CreateScope();

                using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                timeoutCts.CancelAfter(TimeSpan.FromSeconds(30));

                object? eventValue = null;
                try
                {
                    var handler = scope.ServiceProvider.GetRequiredService(currentHandlerType);

                    if (handler is IIntegrationEventHandler integrationEventHandler)
                    {
                        eventValue = JsonSerializer.Deserialize(ea.Body.Span, eventType);

                        if (logger.IsEnabled(LogLevel.Debug))
                            logger.LogDebug("Received {eventName} {@eventValue}", eventName, eventValue);

                        if (eventValue is IIntegrationEvent integrationEvent)
                            await integrationEventHandler.HandleAsync(integrationEvent, timeoutCts.Token);

                        if (logger.IsEnabled(LogLevel.Debug))
                            logger.LogDebug("Handled {eventName} {@eventValue}", eventName, eventValue);
                    }
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: ct);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "EventBus - Error handilng {eventName} {@eventValue}", eventName, eventValue);
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false, cancellationToken: ct);
                    await Task.Delay(1000, ct);
                }
            };

            await channel.BasicConsumeAsync(queue: eventName, autoAck: false, consumer: consumer, cancellationToken: ct);
        }

        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(Timeout.Infinite, ct);
        }
    }
}
