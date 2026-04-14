using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Abstractions.EventBus;
using XMS.Core.Common;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

internal class Document_СписаниеБезналичныхДенежныхСредств_EventHandler(
    UtClient utClient,
    IDbContextFactoryProxy dbFactory,
    IEventPublisher eventPublisher,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_EventHandler> logger,
    IHostEnvironment hostEnvironment)
    : BaseService, IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler
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

        await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);

        string basicExchangeName = hostEnvironment.IsDevelopment()
            ? $"dev_{nameof(Document_СписаниеБезналичныхДенежныхСредств)}"
            : $"{nameof(Document_СписаниеБезналичныхДенежныхСредств)}";

        if (!fetchedItem.DeletionMark && fetchedItem.Posted)
        {
            await dbContext.Set<Document_СписаниеБезналичныхДенежныхСредств>().AddAsync(fetchedItem, ct);

            await dbContext.SaveChangesAsync(ct);

            await eventPublisher.PublishAsync($"{basicExchangeName}_received", Document_СписаниеБезналичныхДенежныхСредств_Dto.From(fetchedItem));
        }
        else
        {
            await eventPublisher.PublishAsync($"{basicExchangeName}_deleted", Document_СписаниеБезналичныхДенежныхСредств_Dto.From(fetchedItem));
        }

        logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleEvent), oneCNotifyMessage, fetchedItem);

        return ServiceResult.Success();
    }

    private async Task<Document_СписаниеБезналичныхДенежныхСредств?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct)
    {
        var uri = IntegrationHelper.GetUriByRefKey<Document_СписаниеБезналичныхДенежныхСредств>(refKey);

        var rootObject = await utClient.GetValueAsync<RootObject<Document_СписаниеБезналичныхДенежныхСредств>>(uri, ct);

        var result = rootObject?.Value?.FirstOrDefault();

        return result;
    }
}
