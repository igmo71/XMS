using Microsoft.Extensions.Logging;
using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature.Catalog_КСЗ_КатегорииЗатрат;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

internal class Catalog_КСЗ_КатегорииЗатрат_OutboundHandler(
    UtClient utClient,
    ILogger<Entity> logger) : BaseService, ICatalog_КСЗ_КатегорииЗатрат_OutboundHandler
{
    public async Task HandleAsync(Entity entityEvent, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        logger.LogDebug("{Source} - Start {@entityEvent}", nameof(HandleAsync), entityEvent);

        var fetchedItem = await utClient.FetchByRefKeyAsync<Entity>(entityEvent.Ref_Key, ct);

        if (fetchedItem is null)
        {
            logger.LogDebug("{Source} - Not Exists {@entityEvent}", nameof(HandleAsync), entityEvent);
            var created = await utClient.PostValueAsync(entityEvent, typeof(Entity).Name, ct);
            logger.LogDebug("{Source} - Created {@created}", nameof(HandleAsync), created);
        }
        else
        {
            logger.LogDebug("{Source} - Exists {@entityEvent}", nameof(HandleAsync), entityEvent);
            var updated = await utClient.PatchValueAsync(entityEvent, typeof(Entity).Name, ct);
            logger.LogDebug("{Source} - Updated {@updated}", nameof(HandleAsync), updated);
        }
    }
}
