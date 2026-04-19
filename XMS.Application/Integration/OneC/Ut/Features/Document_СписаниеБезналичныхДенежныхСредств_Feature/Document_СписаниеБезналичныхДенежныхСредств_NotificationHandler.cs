using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DeletedEvent = XMS.Application.EventBus.Events.Document_СписаниеБезналичныхДенежныхСредств_Deleted;
using Entity = XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств;
using ReceivedEvent = XMS.Application.EventBus.Events.Document_СписаниеБезналичныхДенежныхСредств_Received;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

public record Document_СписаниеБезналичныхДенежныхСредств_Notification : DocumentNotification;

internal class Document_СписаниеБезналичныхДенежныхСредств_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher eventPublisher,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_NotificationHandler> logger)
    : BaseService, IIntegrationEventHandler<Document_СписаниеБезналичныхДенежныхСредств_Notification>
{
    public async Task HandleAsync(Document_СписаниеБезналичныхДенежныхСредств_Notification oneCNotifyMessage, CancellationToken ct = default)
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

            var receivedEvent = new ReceivedEvent
            {
                Ref_Key = fetchedItem.Ref_Key,
                Date = fetchedItem.Date,
                Number = fetchedItem.Number,
                СуммаДокумента = fetchedItem.СуммаДокумента,
                Автор_Key = fetchedItem.Автор_Key,
                КСЗ_КатегорияЗатрат_Key = fetchedItem.КСЗ_КатегорияЗатрат_Key,
                СтатьяДвиженияДенежныхСредств_Key = fetchedItem.СтатьяДвиженияДенежныхСредств_Key,
                ХозяйственнаяОперация = fetchedItem.ХозяйственнаяОперация,
                НазначениеПлатежа = fetchedItem.НазначениеПлатежа,
                Комментарий = fetchedItem.Комментарий
            };
            await eventPublisher.PublishAsync(receivedEvent, ct);
        }
        else
        {
            var deletedEvent = new DeletedEvent
            {
                Ref_Key = fetchedItem.Ref_Key,
                Date = fetchedItem.Date,
                Number = fetchedItem.Number,
                СуммаДокумента = fetchedItem.СуммаДокумента,
                Автор_Key = fetchedItem.Автор_Key,
                КСЗ_КатегорияЗатрат_Key = fetchedItem.КСЗ_КатегорияЗатрат_Key,
                СтатьяДвиженияДенежныхСредств_Key = fetchedItem.СтатьяДвиженияДенежныхСредств_Key,
                ХозяйственнаяОперация = fetchedItem.ХозяйственнаяОперация,
                НазначениеПлатежа = fetchedItem.НазначениеПлатежа,
                Комментарий = fetchedItem.Комментарий
            };
            await eventPublisher.PublishAsync(deletedEvent, ct);
        }

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleAsync), oneCNotifyMessage, fetchedItem);
    }
}
