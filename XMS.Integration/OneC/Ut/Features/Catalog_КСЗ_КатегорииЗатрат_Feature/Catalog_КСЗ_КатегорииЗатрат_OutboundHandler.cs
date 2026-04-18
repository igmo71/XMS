using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.EventBus;
using XMS.Core.Common;
using XMS.EventBus.Abstractions;
using XMS.EventBus.Events;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

internal class Catalog_КСЗ_КатегорииЗатрат_OutboundHandler(
    UtClient utClient,
    ILogger<Catalog_КСЗ_КатегорииЗатрат_OutboundHandler> logger)
    : BaseService, IAppEventHandler<CostCategoryCommonEvent>
{
    public async Task HandleAsync(CostCategoryCommonEvent outboundEvent, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        logger.LogDebug("{Source} - Start {@entityEvent}", nameof(HandleAsync), outboundEvent);

        var uri = $"Catalog_КСЗ_КатегорииЗатрат?$format=json&$inlinecount=allpages&$filter=Ref_Key eq guid'{outboundEvent.Ref_Key}'";
        var fetchedItem = await utClient.FetchByRefKeyAsync<Catalog_КСЗ_КатегорииЗатрат>(outboundEvent.Ref_Key, ct);

        if (fetchedItem is null)
        {
            logger.LogDebug("{Source} - Not Exists {@entityEvent}", nameof(HandleAsync), outboundEvent);
            var created = await utClient.PostValueAsync(outboundEvent, typeof(Catalog_КСЗ_КатегорииЗатрат).Name, ct);
            logger.LogDebug("{Source} - Created {@created}", nameof(HandleAsync), created);
        }
        else
        {
            logger.LogDebug("{Source} - Exists {@entityEvent}", nameof(HandleAsync), outboundEvent);
            var updated = await utClient.PatchValueAsync(new Catalog_КСЗ_КатегорииЗатрат
            {
                Ref_Key = outboundEvent.Ref_Key,
                DataVersion = outboundEvent.DataVersion,
                DeletionMark = outboundEvent.DeletionMark,
                Description = outboundEvent.Description,
                Parent_Key = outboundEvent.Parent_Key
            }, typeof(Catalog_КСЗ_КатегорииЗатрат).Name, ct);
            logger.LogDebug("{Source} - Updated {@updated}", nameof(HandleAsync), updated);
        }
    }
}
