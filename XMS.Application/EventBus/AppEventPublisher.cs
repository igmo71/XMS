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
        using var scope = scopeFactory.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IAppEventHandler<TEvent>>();

        if (handlers == null || !handlers.Any())
            return;

        //foreach (var handler in handlers)
        //{
        //    await handler.HandleAsync(eventValue, ct);
        //}

        var tasks = handlers.Select(async handler =>
        {
            try
            {
                await handler.HandleAsync(eventValue, ct);
            }
            catch (Exception)
            {
                logger.LogError("{Source} Error handling {EventType} with handler {HandlerType}",
                    nameof(PublishAsync), typeof(TEvent).Name, handler.GetType().Name);
            }
        });

        await Task.WhenAll(tasks);
    }
}