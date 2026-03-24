using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

public class Catalog_Партнеры_Worker(IConnectionFactory factory, IServiceProvider serviceProvider, ILogger<Catalog_Партнеры_Worker> logger)
    : OneCCatalogWorker<Catalog_Партнеры, Catalog_Партнеры_Changed, ICatalog_Партнеры_Service>(factory, serviceProvider, logger)
{
    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    using var connection = await factory.CreateConnectionAsync(stoppingToken);
    //    using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

    //    string queueName = nameof(Catalog_Партнеры);
    //    string exchangeName = nameof(Catalog_Партнеры);

    //    await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout, durable: true, cancellationToken: stoppingToken);
    //    await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);
    //    await channel.QueueBindAsync(queueName, exchangeName, routingKey: string.Empty, cancellationToken: stoppingToken);

    //    var consumer = new AsyncEventingBasicConsumer(channel);

    //    consumer.ReceivedAsync += async (model, ea) =>
    //    {
    //        using var scope = serviceProvider.CreateScope();
    //        var catalogService = scope.ServiceProvider.GetRequiredService<ICatalog_Партнеры_Service>();

    //        var body = ea.Body.ToArray();
    //        var jsonMessage = Encoding.UTF8.GetString(body);

    //        try
    //        {
    //            var message = JsonSerializer.Deserialize<Catalog_Партнеры_Changed>(jsonMessage);
    //            if (message != null)
    //            {
    //                await catalogService.HandleEventOneC(message);
    //            }
    //            await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.LogError(ex, "{jsonMessage}", jsonMessage);
    //            await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
    //        }
    //    };

    //    await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

    //    await Task.Delay(Timeout.Infinite, stoppingToken);
    //}
}