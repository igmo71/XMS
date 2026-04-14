using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Abstractions.EventBus;
using XMS.Core.Common;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

internal class Document_РасходныйКассовыйОрдер_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IEventPublisher eventPublisher,
    ILogger<Document_РасходныйКассовыйОрдер_EventHandler> logger)
    : BaseService, IDocument_РасходныйКассовыйОрдер_EventHandler
{
    public async Task<ServiceResult> HandleEvent(DocumentEvent oneCNotifyMessage, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        logger.LogDebug("{Source} - Start {@message}", nameof(HandleEvent), oneCNotifyMessage);

        var fetchedItem = await FetchByRefKeyAsync(oneCNotifyMessage.Ref_Key, ct);

        if (fetchedItem is null)
        {
            logger.LogError("{Source} - Failed to feath {@message}", nameof(HandleEvent), oneCNotifyMessage);
            return ServiceError.NotFound;
        }

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<Document_РасходныйКассовыйОрдер>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);

        if (!fetchedItem.DeletionMark && fetchedItem.Posted)
        {
            await dbContext.Set<Document_РасходныйКассовыйОрдер>().AddAsync(fetchedItem, ct);

            await dbContext.SaveChangesAsync(ct);

            await eventPublisher.PublishAsync($"{nameof(Document_РасходныйКассовыйОрдер)}_received", Document_РасходныйКассовыйОрдер_Dto.From(fetchedItem));
        }
        else
        {
            await eventPublisher.PublishAsync($"{nameof(Document_РасходныйКассовыйОрдер)}_deleted", Document_РасходныйКассовыйОрдер_Dto.From(fetchedItem));
        }

        logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleEvent), oneCNotifyMessage, fetchedItem);

        return ServiceResult.Success();
    }

    private async Task<Document_РасходныйКассовыйОрдер?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct)
    {
        var uri = IntegrationHelper.GetUriByRefKey<Document_РасходныйКассовыйОрдер>(refKey);

        var rootObject = await utClient.GetValueAsync<RootObject<Document_РасходныйКассовыйОрдер>>(uri, ct);

        var result = rootObject?.Value?.FirstOrDefault();

        return result;
    }
}
