using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.Data;
using XMS.Core.Common;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut.Services
{
    internal class Catalog_СтатьиДвиженияДенежныхСредств_Service(
        UtClient utClient,
        IDbContextFactoryProxy dbFactory,
        ILogger<Catalog_СтатьиДвиженияДенежныхСредств_Service> logger)
        : BaseService, ICatalog_СтатьиДвиженияДенежныхСредств_Service
    {
        public async Task<ServiceResult> CreateOrUpdateAsync(Guid refKey, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var newItem = await FetchByRefKeyAsync(refKey, ct);

            if (newItem is null)
                return ServiceError.InvalidOperation.WithDescription(
                    $"Failed to feath {nameof(Catalog_СтатьиДвиженияДенежныхСредств)} by {refKey}");

            await dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>()
                .Where(e => e.Ref_Key == refKey)
                .ExecuteDeleteAsync(cancellationToken: ct);

            await dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>()
                .AddAsync(newItem, ct);

            await dbContext.SaveChangesAsync(ct);

            logger.LogDebug("{Source} {refKey} {newItem}", nameof(CreateOrUpdateAsync), refKey, newItem);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(Guid refKey, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>()
                .Where(e => e.Ref_Key == refKey)
                .ExecuteDeleteAsync(cancellationToken: ct);

            logger.LogDebug("{Source} {refKey}", nameof(DeleteAsync), refKey);

            return ServiceResult.Success();
        }

        public async Task<Catalog_СтатьиДвиженияДенежныхСредств?> GetAsync(Guid refKey, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>()
                .FindAsync([refKey], cancellationToken: ct);

            return result;
        }

        public async Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>()
                .AsNoTracking()
                .HandleCatalogQuery(parameters)
                .ToListAsync(ct);

            return result ?? [];
        }

        public async Task<ServiceResult> ResyncAsync(CancellationToken ct)
        {
            StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>()
                .ExecuteDeleteAsync(ct);

            var items = await FetchListAsync(ct);

            await dbContext.Set<Catalog_СтатьиДвиженияДенежныхСредств>()
                .AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        private async Task<Catalog_СтатьиДвиженияДенежныхСредств?> FetchByRefKeyAsync(Guid refKey, CancellationToken ct = default)
        {
            var uri = Catalog_СтатьиДвиженияДенежныхСредств.GetUriByRefKey(refKey);

            var rootObject = await utClient.GetValueAsync<RootObject<Catalog_СтатьиДвиженияДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?[0];

            return result;
        }

        private async Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> FetchListAsync(CancellationToken ct = default)
        {
            var uri = Catalog_СтатьиДвиженияДенежныхСредств.Uri;

            var rootObject = await utClient.GetValueAsync<RootObject<Catalog_СтатьиДвиженияДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?.ToList();

            return result ?? [];
        }
    }
}
