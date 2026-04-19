using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DeletedEvent = XMS.Application.EventBus.Events.Document_РасходныйКассовыйОрдер_Deleted;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер;
using ReceivedEvent = XMS.Application.EventBus.Events.Document_РасходныйКассовыйОрдер_Received;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

public record Document_РасходныйКассовыйОрдер_Notification : DocumentNotification;

internal class Document_РасходныйКассовыйОрдер_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Document_РасходныйКассовыйОрдер_NotificationHandler> logger)
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

            var receivedEvent = new DeletedEvent()
            {
                Ref_Key = fetchedItem.Ref_Key,
                Date = fetchedItem.Date,
                Number = fetchedItem.Number,
                СуммаДокумента = fetchedItem.СуммаДокумента,
                Автор_Key = fetchedItem.Автор_Key,
                КСЗ_КатегорияЗатрат_Key = fetchedItem.КСЗ_КатегорияЗатрат_Key,
                СтатьяДвиженияДенежныхСредств_Key = fetchedItem.СтатьяДвиженияДенежныхСредств_Key,
                ХозяйственнаяОперация = fetchedItem.ХозяйственнаяОперация,
                Комментарий = fetchedItem.Комментарий
            };
            await appEventPublisher.PublishAsync(receivedEvent, ct);
        }
        else
        {
            var deletedEvent = new ReceivedEvent()
            {
                Ref_Key = fetchedItem.Ref_Key,
                Date = fetchedItem.Date,
                Number = fetchedItem.Number,
                СуммаДокумента = fetchedItem.СуммаДокумента,
                Автор_Key = fetchedItem.Автор_Key,
                КСЗ_КатегорияЗатрат_Key = fetchedItem.КСЗ_КатегорияЗатрат_Key,
                СтатьяДвиженияДенежныхСредств_Key = fetchedItem.СтатьяДвиженияДенежныхСредств_Key,
                ХозяйственнаяОперация = fetchedItem.ХозяйственнаяОперация,
                Комментарий = fetchedItem.Комментарий
            };
            await appEventPublisher.PublishAsync(deletedEvent, ct);
        }

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleAsync), oneCNotifyMessage, fetchedItem);
    }
}
