using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace XMS.Infrastructure.EventBus;

internal class RabbitMqConsumer(
    IServiceProvider serviceProvider,
    IConnectionFactory connectionFactory,
    IHostEnvironment hostEnvironment,
    ILogger<RabbitMqConsumer> logger,
    List<Type> handlers) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using var connection = await connectionFactory.CreateConnectionAsync(ct);
        using var channel = await connection.CreateChannelAsync(cancellationToken: ct);

        foreach (var interfaceType in handlers)
        {
            var currentInterface = interfaceType;
            var eventType = currentInterface.GetGenericArguments()[0];

            var prefix = hostEnvironment.IsDevelopment() ? "dev" : "xms";
            string eventName = $"{prefix}.{eventType.Name}.notify";

            await channel.ExchangeDeclareAsync(exchange: eventName, ExchangeType.Fanout, durable: true, cancellationToken: ct);
            await channel.QueueDeclareAsync(queue: eventName, durable: true, exclusive: false, autoDelete: false, cancellationToken: ct);
            await channel.QueueBindAsync(queue: eventName, exchange: eventName, routingKey: string.Empty, cancellationToken: ct);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                using var scope = serviceProvider.CreateScope();

                var handler = scope.ServiceProvider.GetRequiredService(currentInterface);

                var eventValue = JsonSerializer.Deserialize(ea.Body.Span, eventType);
                try
                {
                    if (eventValue != null)
                        await ((dynamic)handler).HandleAsync((dynamic)eventValue, ct);
                    else
                        logger.LogWarning("Received null event for {eventType}", eventType.Name);

                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: ct);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{@eventValue}", eventValue);
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: ct);
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
