using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.EventBus;

namespace XMS.Application.EventBus;

public class AppEventPublisher(
    AppEventChannel appEventChannel,
    ILogger<AppEventPublisher> logger) : IAppEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent eventValue, CancellationToken ct) where TEvent : class, IAppEvent
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Write to channel {@eventValue}", nameof(PublishAsync), eventValue);

        await appEventChannel.WriteAsync(eventValue, ct);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Enqueued {@eventValue}", nameof(PublishAsync), eventValue);
    }
}
