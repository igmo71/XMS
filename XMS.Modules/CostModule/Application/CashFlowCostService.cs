using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Core.Abstractions.Data;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application
{
    internal class CashFlowCostService(IDbContextFactoryProxy dbFactory) : ICashFlowCostService
    {
        public async Task AddRangeCashFlowCostAsync(List<CostCatalog_СтатьиДвиженияДенежныхСредств> items, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CostCatalog_СтатьиДвиженияДенежныхСредств>().AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateRangeCashFlowCostAsync(List<CostCatalog_СтатьиДвиженияДенежныхСредств> selectedItems, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existingItems = await dbContext.Set<CostCatalog_СтатьиДвиженияДенежныхСредств>()
                .Where(e => e.CostCategoryItemId == selectedItems[0].CostCategoryItemId)
                .ToListAsync(ct);

            var existingItemIds = existingItems.Select(e => e.Id);
            var selectedItemIds = selectedItems.Select(e => e.Id);

            var toRemove = existingItems.Where(e => !selectedItemIds.Contains(e.Id)).ToList();
            if (toRemove?.Count > 0)
                dbContext.Set<CostCatalog_СтатьиДвиженияДенежныхСредств>().RemoveRange(toRemove);

            var toAdd = selectedItems.Where(e => !existingItemIds.Contains(e.Id)).ToList();
            if (toAdd?.Count > 0)
                await dbContext.Set<CostCatalog_СтатьиДвиженияДенежныхСредств>().AddRangeAsync(toAdd, ct);


            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteCashFlowCostAsync(Guid itemId, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CostCatalog_СтатьиДвиженияДенежныхСредств>()
                .Where(x => x.Id == itemId)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<IReadOnlyList<CostCatalog_СтатьиДвиженияДенежныхСредств>> GetListAsync(CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CostCatalog_СтатьиДвиженияДенежныхСредств>()
                .AsNoTracking()
                .Include(e => e.CashFlowItem)
                .ToListAsync(cancellationToken: ct);

            return result;
        }

        public async Task<HashSet<Guid>> GetSelectedCashFlowItemIds(Guid costCategoryItemId)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CostCatalog_СтатьиДвиженияДенежныхСредств>()
                .AsNoTracking()
                .Where(e => e.CostCategoryItemId == costCategoryItemId)
                .Select(e => e.CatalogRefKey)
                .ToHashSetAsync();

            return result;
        }
    }
}
