using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut;

namespace XMS.Integration.OneC
{
    internal abstract class OneCCatalogService<TEntity, TEvent>(
        UtClient utClient,
        IDbContextFactoryProxy dbFactory,
        ILogger logger) : BaseService, IOneCCatalogService<TEntity, TEvent>
        where TEntity : class, IOneCCatalog
        where TEvent : CatalogNotifyMessage
    {

        public async Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<TEntity>()
                .FindAsync([refKey], cancellationToken: ct);

            return result;
        }

        public async Task<IReadOnlyList<TEntity>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<TEntity>()
                .AsNoTracking()
                .HandleCatalogQuery(parameters)
                .ToListAsync(ct);

            return result ?? [];
        }

        public async Task<ServiceResult> HandleEventOneC(TEvent oneCNotifyMessage, CancellationToken ct = default)
        {
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

            if (!fetchedItem.DeletionMark)
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

            var rootObject = await utClient.GetValueAsync<RootObject<TEntity>>(uri, ct);

            var result = rootObject?.Value?.FirstOrDefault();

            return result;
        }

        public async Task<ServiceResult> ResyncAsync(CancellationToken ct = default)
        {
            StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<TEntity>()
                .ExecuteDeleteAsync(ct);

            var items = await FetchListAsync(ct);

            await dbContext.Set<TEntity>()
                .AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        private async Task<IReadOnlyList<TEntity>> FetchListAsync(CancellationToken ct)
        {
            var uri = TEntity.Uri;

            var rootObject = await utClient.GetValueAsync<RootObject<TEntity>>(uri, ct);

            var result = rootObject?.Value?.ToList();

            return result ?? [];
        }
    }
}
