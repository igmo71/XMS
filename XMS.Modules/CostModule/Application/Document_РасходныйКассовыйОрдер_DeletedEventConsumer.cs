using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Modules.CostModule.Abstractions;

namespace XMS.Modules.CostModule.Application;

internal class Document_РасходныйКассовыйОрдер_DeletedEventConsumer(
    IConnectionFactory factory,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    ILogger<Document_РасходныйКассовыйОрдер_ReceivedEventConsumer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var connection = await factory.CreateConnectionAsync(stoppingToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        string exchangeName = $"{nameof(Document_РасходныйКассовыйОрдер)}_deleted";
        string queueName = $"{nameof(Document_РасходныйКассовыйОрдер)}_deleted";

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
                var handler = scope.ServiceProvider.GetRequiredService<ICostAllocationService>();

                var dto = JsonSerializer.Deserialize<Document_РасходныйКассовыйОрдер_Dto>(jsonMessage);
                if (dto != null)
                {
                    await handler.HandleDocument_РасходныйКассовыйОрдер_DeletedAsync(dto);
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
