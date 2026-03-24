using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC
{
    public abstract class OneCCatalogWorker<TEntity, TEvent, TService>(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    ILogger logger) : BackgroundService
        where TEntity : class, IOneCCatalog
        where TEvent : CatalogNotifyMessage
        where TService : class, IOneCCatalogService<TEntity, TEvent>
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = await factory.CreateConnectionAsync(stoppingToken);
            using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            string queueName = TEntity.QueueName;
            string exchangeName = TEntity.QueueName;

            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout, durable: true, cancellationToken: stoppingToken);
            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);
            await channel.QueueBindAsync(queueName, exchangeName, routingKey: string.Empty, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                using var scope = serviceProvider.CreateScope();
                var catalogService = scope.ServiceProvider.GetRequiredService<TService>();

                var body = ea.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);

                try
                {
                    var message = JsonSerializer.Deserialize<TEvent>(jsonMessage);
                    if (message != null)
                    {
                        await catalogService.HandleEventOneC(message);
                    }
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{jsonMessage}", jsonMessage);
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            };

            await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
