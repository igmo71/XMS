using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_NotificationHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IAppEventPublisher appEventPublisher,
    ILogger<Document_РасходныйКассовыйОрдер_NotificationHandler> logger)
    : BaseService, IIntegrationEventHandler<Document_РасходныйКассовыйОрдер>
{
    public async Task HandleAsync(Document_РасходныйКассовыйОрдер oneCNotifyMessage, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@message}", nameof(HandleAsync), oneCNotifyMessage);

        var fetchedItem = await utClient.FetchByRefKeyAsync<Document_РасходныйКассовыйОрдер>(oneCNotifyMessage.Ref_Key, ct);

        if (fetchedItem is null)
        {
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning("{Source} - Failed to feath {@message}", nameof(HandleAsync), oneCNotifyMessage);
            return;
        }

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<Document_РасходныйКассовыйОрдер>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);

        if (!fetchedItem.DeletionMark && fetchedItem.Posted)
        {
            await dbContext.Set<Document_РасходныйКассовыйОрдер>().AddAsync(fetchedItem, ct);
            await dbContext.SaveChangesAsync(ct);

            var receivedEvent = new Document_РасходныйКассовыйОрдер_Received()
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
            var deletedEvent = new Document_РасходныйКассовыйОрдер_Deleted()
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
