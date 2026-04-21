using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Common;
using XMS.Application.EventBus.Events;
using XMS.Application.Integration.OneC.Ut.ODataClient;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

internal class Catalog_КСЗ_КатегорииЗатрат_OutboundHandler(
    UtClient utClient,
    ILogger<Catalog_КСЗ_КатегорииЗатрат_OutboundHandler> logger)
    : BaseService, IAppEventHandler<CostCategoryCommonEvent>
{
    public async Task HandleAsync(CostCategoryCommonEvent outboundEvent, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@entityEvent}", nameof(HandleAsync), outboundEvent);

        var uri = $"Catalog_КСЗ_КатегорииЗатрат?$format=json&$inlinecount=allpages&$filter=Ref_Key eq guid'{outboundEvent.Ref_Key}'";
        var fetchedItem = await utClient.FetchByRefKeyAsync<Catalog_КСЗ_КатегорииЗатрат>(outboundEvent.Ref_Key, ct);

        if (fetchedItem is null)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - Not Exists {@entityEvent}", nameof(HandleAsync), outboundEvent);
            var created = await utClient.PostValueAsync(outboundEvent, typeof(Catalog_КСЗ_КатегорииЗатрат).Name, ct);
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - Created {@created}", nameof(HandleAsync), created);
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - Exists {@entityEvent}", nameof(HandleAsync), outboundEvent);
            var updated = await utClient.PatchValueAsync(new Catalog_КСЗ_КатегорииЗатрат
            {
                Ref_Key = outboundEvent.Ref_Key,
                DataVersion = outboundEvent.DataVersion,
                DeletionMark = outboundEvent.DeletionMark,
                Description = outboundEvent.Description,
                Parent_Key = outboundEvent.Parent_Key
            }, typeof(Catalog_КСЗ_КатегорииЗатрат).Name, ct);
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - Updated {@updated}", nameof(HandleAsync), updated);
        }
    }
}
