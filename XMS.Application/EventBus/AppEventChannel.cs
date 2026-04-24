using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using XMS.Application.Abstractions.EventBus;

namespace XMS.Application.EventBus;

public sealed class AppEventChannel(
    IServiceScopeFactory scopeFactory,
    ILogger<AppEventChannel> logger) : BackgroundService
{
    private readonly Channel<IAppEventEnvelope> _channel = Channel.CreateUnbounded<IAppEventEnvelope>(new UnboundedChannelOptions
    {
        SingleReader = true,
        SingleWriter = false,
        AllowSynchronousContinuations = false
    });

    public ValueTask WriteAsync<TEvent>(TEvent eventValue, CancellationToken ct = default)
        where TEvent : class, IAppEvent
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Enqueue {EventType} {@EventValue}", nameof(WriteAsync), typeof(TEvent).Name, eventValue);

        return _channel.Writer.WriteAsync(new AppEventEnvelope<TEvent>(eventValue), ct);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var envelope in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await envelope.DispatchAsync(scopeFactory, logger, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError(ex, "{Source} - Error dispatching app event from channel", nameof(ExecuteAsync));
            }
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Writer.TryComplete();
        return base.StopAsync(cancellationToken);
    }

    private interface IAppEventEnvelope
    {
        Task DispatchAsync(IServiceScopeFactory scopeFactory, ILogger logger, CancellationToken ct);
    }

    private sealed class AppEventEnvelope<TEvent>(TEvent eventValue) : IAppEventEnvelope
        where TEvent : class, IAppEvent
    {
        public async Task DispatchAsync(IServiceScopeFactory scopeFactory, ILogger logger, CancellationToken ct)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - Start {EventType} {@EventValue}", nameof(DispatchAsync), typeof(TEvent).Name, eventValue);

            using var scope = scopeFactory.CreateScope();

            var handlers = scope.ServiceProvider.GetServices<IAppEventHandler<TEvent>>().ToArray();

            if (handlers.Length == 0)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                    logger.LogDebug("{Source} - No handlers for {EventType}", nameof(DispatchAsync), typeof(TEvent).Name);

                return;
            }

            var exceptions = new List<Exception>();

            foreach (var handler in handlers)
            {
                try
                {
                    if (logger.IsEnabled(LogLevel.Debug))
                        logger.LogDebug("{Source} {HandlerType} {@EventValue}", nameof(DispatchAsync), handler.GetType().Name, eventValue);

                    await handler.HandleAsync(eventValue, ct);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);

                    if (logger.IsEnabled(LogLevel.Error))
                        logger.LogError(ex, "{Source} Error handling {EventType} with handler {HandlerType}",
                            nameof(DispatchAsync), typeof(TEvent).Name, handler.GetType().Name);
                }
            }

            if (exceptions.Count == 0)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                    logger.LogDebug("{Source} - Ok {EventType} {@EventValue}", nameof(DispatchAsync), typeof(TEvent).Name, eventValue);

                return;
            }

            if (logger.IsEnabled(LogLevel.Error))
                logger.LogError("{Source} - {ErrorCount} handler errors for {EventType}",
                    nameof(DispatchAsync), exceptions.Count, typeof(TEvent).Name);
        }
    }
}
