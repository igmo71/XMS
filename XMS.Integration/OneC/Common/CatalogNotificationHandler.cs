using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC.Common;

internal abstract class CatalogNotificationHandler<TEntity, TEvent>(UtClient utClient, IDbContextFactoryProxy dbFactory, ILogger logger)
    : BaseService, IIntegrationEventHandler<TEvent>
    where TEntity : class, ICatalog, ISyncable
    where TEvent : class, IIntegrationEvent
{
    public async Task HandleAsync(TEvent oneCNotifyMessage, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        logger.LogDebug("{Source} - Start {@message}", nameof(HandleAsync), oneCNotifyMessage);

        var fetchedItem = await FetchByRefKeyAsync(oneCNotifyMessage.Ref_Key, ct);

        if (fetchedItem is null)
        {
            logger.LogError("{Source} - Failed to feath {@message}", nameof(HandleAsync), oneCNotifyMessage);
        }

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<TEntity>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);

        await dbContext.Set<TEntity>().AddAsync(fetchedItem, ct);

        await dbContext.SaveChangesAsync(ct);

        logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleAsync), oneCNotifyMessage, fetchedItem);
    }

    private async Task<TEntity?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct)
    {
        var uri = IntegrationHelper.GetUriByRefKey<TEntity>(refKey);

        var rootObject = await utClient.GetValueAsync<RootObject<TEntity>>(uri, ct);

        var result = rootObject?.Value?.FirstOrDefault();

        return result;
    }
}
