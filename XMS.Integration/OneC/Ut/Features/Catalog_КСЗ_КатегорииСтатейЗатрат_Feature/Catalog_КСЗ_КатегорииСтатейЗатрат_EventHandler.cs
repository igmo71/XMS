using Microsoft.Extensions.Logging;
using XMS.Core.Common;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;
using Entity = XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииСтатейЗатрат_Feature.Catalog_КСЗ_КатегорииСтатейЗатрат;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииСтатейЗатрат_Feature;

internal class Catalog_КСЗ_КатегорииСтатейЗатрат_EventHandler(
    UtClient utClient,
    ILogger<Entity> logger) : BaseService, ICatalog_КСЗ_КатегорииСтатейЗатрат_EventHandler
{
    public async Task<ServiceResult> HandleEvent(Entity entityEvent, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        logger.LogDebug("{Source} - Start {@entityEvent}", nameof(HandleEvent), entityEvent);

        var fetchedItem = await FetchByRefKeyAsync(entityEvent.Ref_Key, ct);

        if (fetchedItem is null)
        {
            logger.LogDebug("{Source} - Not Exists {@entityEvent}", nameof(HandleEvent), entityEvent);
            var created = await utClient.PostValueAsync(entityEvent, typeof(Entity).Name);
            logger.LogDebug("{Source} - Created {@created}", nameof(HandleEvent), created);
        }
        else
        {
            logger.LogDebug("{Source} - Not Exists {@entityEvent}", nameof(HandleEvent), entityEvent);
            var updated = await utClient.PatchValueAsync(entityEvent, typeof(Entity).Name);
            logger.LogDebug("{Source} - Updated {@updated}", nameof(HandleEvent), updated);
        }

        return ServiceResult.Success();
    }

    private async Task<Entity> FetchByRefKeyAsync(Guid refKey, CancellationToken ct)
    {
        var uri = IntegrationHelper.GetUriByRefKey<Entity>(refKey);

        var rootObject = await utClient.GetValueAsync<RootObject<Entity>>(uri, ct);

        var result = rootObject?.Value?.FirstOrDefault();

        return result;
    }
}
