using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.ODataClient;

namespace XMS.Integration.OneC;

internal class DocumentEventHandler<TEntity, TEvent>(UtClient utClient, IDbContextFactoryProxy dbFactory, ILogger logger)
    : BaseService, IOneCEventHandler<TEvent>
    where TEntity : class, IDocument
    where TEvent : class, IOneCEvent
{
    public async Task<ServiceResult> HandleEventOneC(TEvent oneCNotifyMessage, CancellationToken ct = default)
    {
        StartActivity();

        logger.LogDebug("{Source} - Start {@message}", nameof(HandleEventOneC), oneCNotifyMessage);

        var fetchedItem = await FetchByRefKeyAsync(oneCNotifyMessage.Ref_Key, ct);

        if (fetchedItem is null)
        {
            logger.LogError("{Source} - Failed to feath {@message}", nameof(HandleEventOneC), oneCNotifyMessage);
            return ServiceError.NotFound;
        }

        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<TEntity>()
            .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
            .ExecuteDeleteAsync(ct);

        if (!fetchedItem.DeletionMark && fetchedItem.Posted)
        {
            await dbContext.Set<TEntity>()
            .AddAsync(fetchedItem, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleEventOneC), oneCNotifyMessage, fetchedItem);

        return ServiceResult.Success();
    }

    private async Task<TEntity?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct)
    {
        var uri = TEntity.GetUriByRefKey(refKey);

        var rootObject = await utClient.GetValueFromJsonAsync<RootObject<TEntity>>(uri, ct);

        var result = rootObject?.Value?.FirstOrDefault();

        return result;
    }
}
