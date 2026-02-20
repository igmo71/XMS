using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class SkuInventoryUtService(IOneSUtService oneSUtService, IDbContextFactoryProxy dbFactory) : ISkuInventoryUtService
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
            return await oneSUtService.GetStockBalanceUtListAsync(ct);
        }

        public async Task ReloadListAsync(CancellationToken ct = default)
        {
            var list = await LoadListAsync(ct);

            await DeleteAllAsync(ct);

            await AddRangeAsync(list, ct);
        }

        private async Task DeleteAllAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<SkuInventoryUt>().ExecuteDeleteAsync(ct);
        }

        private async Task AddRangeAsync(IReadOnlyList<SkuInventoryUt> list, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<SkuInventoryUt>().AddRangeAsync(list);

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
