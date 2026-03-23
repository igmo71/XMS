using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature
{
    internal class Catalog_Партнеры_Service(
        UtClient utClient,
        IDbContextFactoryProxy dbFactory,
        ILogger<Catalog_Партнеры_Service> logger)
        : BaseService, ICatalog_Партнеры_Service
    {
        public Task<Catalog_Партнеры?> GetAsync(Guid refKey, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Catalog_Партнеры>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> HandleEventOneC(Catalog_Партнеры_Changed oneCNotifyMessage, CancellationToken ct = default)
        {
            logger.LogDebug("{Source} - Start {@message}", nameof(HandleEventOneC), oneCNotifyMessage);

            var fetchedItem = await FetchByRefKeyAsync(oneCNotifyMessage.Ref_Key, ct);

            if (fetchedItem is null)
            {
                logger.LogError("{Source} - Failed to feath {@message}", nameof(HandleEventOneC), oneCNotifyMessage);
                return ServiceError.NotFound;
            }

            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<Catalog_Партнеры>()
                .Where(e => e.Ref_Key == oneCNotifyMessage.Ref_Key)
                .ExecuteDeleteAsync(ct);

            if (!fetchedItem.DeletionMark)
            {
                await dbContext.Set<Catalog_Партнеры>()
                .AddAsync(fetchedItem, ct);

                await dbContext.SaveChangesAsync(ct);
            }

            logger.LogDebug("{Source} - Ok {@message} {@fetchedItem}", nameof(HandleEventOneC), oneCNotifyMessage, fetchedItem);

            return ServiceResult.Success();
        }

        private async Task<Catalog_Партнеры?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct)
        {
            var uri = Catalog_Партнеры.GetUriByRefKey(refKey);

            var rootObject = await utClient.GetValueAsync<RootObject<Catalog_Партнеры>>(uri, ct);

            var result = rootObject?.Value?.FirstOrDefault();

            return result;
        }

        public async Task<ServiceResult> ResyncByDateRangeAsync(CancellationToken ct = default)
        {
            StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<Catalog_Партнеры>()
                .ExecuteDeleteAsync(ct);

            var items = await FetchListAsync(ct);

            await dbContext.Set<Catalog_Партнеры>()
                .AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        private async Task<IReadOnlyList<Catalog_Партнеры>> FetchListAsync(CancellationToken ct)
        {
            var uri = Catalog_Партнеры.Uri;

            var rootObject = await utClient.GetValueAsync<RootObject<Catalog_Партнеры>>(uri, ct);

            var result = rootObject?.Value?.ToList();

            return result ?? [];
        }
    }
}
