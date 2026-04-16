using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Abstractions.EventBus;
using XMS.Core.Common;
using XMS.Integration.Abstractions;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.ODataClient;
using DeletedEvent = XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер_Deleted;
using Entity = XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер;
using ReceivedEvent = XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер_Received;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

public record Document_РасходныйКассовыйОрдер_Notification : DocumentNotification;

internal class Document_РасходныйКассовыйОрдер_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IEventPublisher eventPublisher,
    ILogger<Document_РасходныйКассовыйОрдер_NotificationHandler> logger,
    IEventNamingService eventNaming)
    : BaseService, IIntegrationEventHandler<Document_РасходныйКассовыйОрдер_Notification>
{
    public async Task HandleAsync(Document_РасходныйКассовыйОрдер_Notification oneCNotifyMessage, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@message}", nameof(HandleAsync), oneCNotifyMessage);

        var fetchedItem = await utClient.FetchByRefKeyAsync<Entity>(oneCNotifyMessage.Ref_Key, ct);

        if (fetchedItem is null)
        {
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning("{Source} - Failed to feath {@message}", nameof(HandleAsync), oneCNotifyMessage);
            return;
        }

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<Entity>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);




        if (!fetchedItem.DeletionMark && fetchedItem.Posted)
        {
            await dbContext.Set<Entity>().AddAsync(fetchedItem, ct);
            await dbContext.SaveChangesAsync(ct);

            await eventPublisher.PublishAsync(eventNaming.GetEventName<ReceivedEvent>(), ReceivedEvent.From(fetchedItem), ct);
        }
        else
        {
            await eventPublisher.PublishAsync(eventNaming.GetEventName<DeletedEvent>(), DeletedEvent.From(fetchedItem), ct);
        }

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleAsync), oneCNotifyMessage, fetchedItem);
    }
}
