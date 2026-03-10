using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class CashFlowCostService(IDbContextFactoryProxy dbFactory) : ICashFlowCostService
    {
        public async Task AddRangeCashFlowCostAsync(List<CashFlowCost> items, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CashFlowCost>().AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateRangeCashFlowCostAsync(List<CashFlowCost> selectedItems, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existingItems = await dbContext.Set<CashFlowCost>()
                .Where(e => e.CostCategoryItemId == selectedItems[0].CostCategoryItemId)
                .ToListAsync(ct);

            var existingItemIds = existingItems.Select(e => e.Id);
            var selectedItemIds = selectedItems.Select(e => e.Id);

            var toRemove = existingItems.Where(e => !selectedItemIds.Contains(e.Id)).ToList();
            if (toRemove?.Count > 0)
                dbContext.Set<CashFlowCost>().RemoveRange(toRemove);

            var toAdd = selectedItems.Where(e => !existingItemIds.Contains(e.Id)).ToList();
            if (toAdd?.Count > 0)
                await dbContext.Set<CashFlowCost>().AddRangeAsync(toAdd, ct);


            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteCashFlowCostAsync(Guid itemId, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CashFlowCost>()
                .Where(x => x.Id == itemId)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<IReadOnlyList<CashFlowCost>> GetListAsync(CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CashFlowCost>()
                .AsNoTracking()
                .Include(e => e.CashFlowItem)
                .ToListAsync(cancellationToken: ct);

            return result;
        }

        public async Task<HashSet<Guid>> GetSelectedCashFlowItemIds(Guid costCategoryItemId)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CashFlowCost>()
                .AsNoTracking()
                .Where(e => e.CostCategoryItemId == costCategoryItemId)
                .Select(e => e.CashFlowItemId)
                .ToHashSetAsync();

            return result;
        }
    }
}
