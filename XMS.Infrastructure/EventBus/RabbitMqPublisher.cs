using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text.Json;
using XMS.Core.Abstractions.EventBus;

namespace XMS.Infrastructure.EventBus;

internal class RabbitMqPublisher(IConnectionFactory connectionFactory, ILogger<RabbitMqPublisher> logger) : IEventPublisher, IAsyncDisposable
{
    private IConnection? _connection;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    public async Task PublishAsync<TEvent>(string exchange, TEvent eventValue, CancellationToken ct = default)
    {
        using var connection = await GetConnectionAsync(ct);
        using var channel = await connection.CreateChannelAsync(cancellationToken: ct);

        await channel.ExchangeDeclareAsync(exchange, ExchangeType.Fanout, durable: true, cancellationToken: ct);

        var body = JsonSerializer.SerializeToUtf8Bytes(eventValue);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} {exchange} {eventValue}", nameof(PublishAsync), exchange, eventValue);

        await channel.BasicPublishAsync(exchange: exchange, routingKey: string.Empty, body: body, cancellationToken: ct);
    }

    private async Task<IConnection> GetConnectionAsync(CancellationToken ct)
    {
        if (_connection is { IsOpen: true }) return _connection;

        await _connectionLock.WaitAsync(ct);
        try
        {
            if (_connection is { IsOpen: true }) return _connection;

            _connection = await connectionFactory.CreateConnectionAsync(ct);
            return _connection;
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
            await _connection.DisposeAsync();
        _connectionLock.Dispose();
    }
}
