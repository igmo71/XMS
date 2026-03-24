using RabbitMQ.Client;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using XMS.Core.Abstractions.EventBus;

namespace XMS.Infrastructure.EventBus
{
    internal class RabbitPublisher(IConnectionFactory factory) : IRabbitPublisher
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //WriteIndented = false // Можно поставить true, если хочешь красивый JSON с отступами
        };

        public async Task PublishAsync<T>(string exchange, T message)
        {
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange, ExchangeType.Fanout, durable: true);

            var json = JsonSerializer.Serialize(message, _jsonOptions);
            var body = Encoding.UTF8.GetBytes(json);

            // В 7.x BasicPublishAsync принимает ReadOnlyMemory<byte>
            await channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: string.Empty,
                body: body);
        }
    }
}
