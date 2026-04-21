using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.EventBus;

namespace XMS.Application.EventBus;

public class AppEventPublisher(
    IServiceScopeFactory scopeFactory,
    ILogger<AppEventPublisher> logger) : IAppEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent eventValue, CancellationToken ct) where TEvent : class, IAppEvent
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@eventValue}", nameof(PublishAsync), eventValue);

        using var scope = scopeFactory.CreateScope();

        var handlers = scope.ServiceProvider.GetServices<IAppEventHandler<TEvent>>();

        if (handlers == null || !handlers.Any())
            return;

        foreach (var handler in handlers)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} {handler} {@eventValue}", nameof(PublishAsync), handler.GetType().Name, eventValue);

            await handler.HandleAsync(eventValue, ct);
        }

        //await ExecuteAllHandlers(eventValue, handlers, ct);


        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@eventValue}", nameof(PublishAsync), eventValue);
    }

    private async Task ExecuteAllHandlers<TEvent>(TEvent eventValue, IEnumerable<IAppEventHandler<TEvent>> handlers, CancellationToken ct) where TEvent : class, IAppEvent
    {
        var exceptions = new List<Exception>();

        var tasks = handlers.Select(async handler =>
        {
            try
            {
                await handler.HandleAsync(eventValue, ct);
            }
            catch (Exception ex)
            {
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError(ex, "{Source} Error handling {EventType} with handler {HandlerType}",
                        nameof(PublishAsync), typeof(TEvent).Name, handler.GetType().Name);

                lock (exceptions)
                {
                    exceptions.Add(ex);
                }
            }
        });

        await Task.WhenAll(tasks);

        if (exceptions.Count > 0)
        {
            throw new AggregateException($"One or more handlers failed for {typeof(TEvent).Name}.", exceptions);
        }
    }
}
