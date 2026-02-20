using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class SkuInventoryUtService(
        IOneSUtService oneSUtService,
        IDbContextFactoryProxy dbFactory,
        ILogger<SkuInventoryUtService> logger) : BaseService, ISkuInventoryUtService
    {
        public async Task<IReadOnlyList<SkuInventoryUt>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<SkuInventoryUt>()
                .AsNoTracking()
                .OrderBy(x => x.ScuId)
                .ThenBy(x => x.WarehouseId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<SkuInventoryUt>> LoadListAsync(CancellationToken ct = default)
        {
            using var activity = StartActivity();

            return await oneSUtService.GetStockBalanceUtListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            using var activity = StartActivity();

            var list = await LoadListAsync(ct);

            await DeleteAllAsync(ct);

            await BulkInsertAsync(list, ct);
        }

        private async Task DeleteAllAsync(CancellationToken ct = default)
        {
            using var activity = StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<SkuInventoryUt>().ExecuteDeleteAsync(ct);
        }

        private async Task BulkInsertAsync(IReadOnlyList<SkuInventoryUt> list, CancellationToken ct = default)
        {
            using var activity = StartActivity();

            using var dbContext = dbFactory.CreateDbContext();

            var bulkConfig = new BulkConfig
            {
                
                PropertiesToInclude =
                    [nameof(SkuInventoryUt.Id),
                    nameof(SkuInventoryUt.ScuId),
                    nameof(SkuInventoryUt.WarehouseId),
                    nameof(SkuInventoryUt.QuantityOnHand)]
            };

            try
            {
                await dbContext.BulkInsertAsync(list, bulkConfig, ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception {Messge}", ex.Message);
                throw;
            }
        }
    }
}
