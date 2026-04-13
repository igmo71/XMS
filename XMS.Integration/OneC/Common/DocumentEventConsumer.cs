using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Common;

public abstract class DocumentEventConsumer<TEntity, TEvent, THandler>(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger logger) : BackgroundService
    where TEntity : class, IDocument
    where TEvent : class, IOneCEvent
    where THandler : class, IOneCEventHandler<TEvent>
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var connection = await factory.CreateConnectionAsync(stoppingToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        string queueName = IntegrationHelper.GetQueueName<TEntity>(hostEnvironment);
        string exchangeName = IntegrationHelper.GetExchangeName<TEntity>(hostEnvironment);

        await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout, durable: true, cancellationToken: stoppingToken);
        await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);
        await channel.QueueBindAsync(queueName, exchangeName, routingKey: string.Empty, cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var jsonMessage = Encoding.UTF8.GetString(body);

            try
            {
                using var scope = serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<THandler>();

                var message = JsonSerializer.Deserialize<TEvent>(jsonMessage);
                if (message != null)
                {
                    await handler.HandleEvent(message);
                }
                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{jsonMessage}", jsonMessage);
                await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        };

        await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
