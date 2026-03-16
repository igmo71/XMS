using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application
{
    internal class CostCategoryItemService(IDbContextFactoryProxy dbFactory) : ICostCategoryItemService
    {
        public async Task DeleteByCategoryAsync(Guid categoryId, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CostCategoryItem>()
                .Where(e => e.CategoryId == categoryId).
                ExecuteDeleteAsync(ct);
        }

        public async Task UpdateByCategoryAsync(CostCategory costCategory, IApplicationDbContext? masterDbContext, CancellationToken ct = default)
        {
            if (masterDbContext != null)
            {
                await ExecuteUpdateLogic(costCategory, masterDbContext, ct);
            }
            else
            {
                using var dbContext = dbFactory.CreateDbContext();
                await ExecuteUpdateLogic(costCategory, dbContext, ct);
                await dbContext.SaveChangesAsync(ct);
            }
        }

        private static async Task ExecuteUpdateLogic(CostCategory selectedCostCategory, IApplicationDbContext dbContext, CancellationToken ct)
        {
            var existingValues = await dbContext.Set<CostCategoryItem>()
                .Where(x => x.CategoryId == selectedCostCategory.Id)
                .ToListAsync(ct);

            var existingsItemIds = existingValues.Select(e => e.ItemId).ToHashSet();
            var selectedItemIds = selectedCostCategory.Items?.Select(e => e.Id).ToHashSet();

            var toRemove = existingValues
                .Where(e => selectedItemIds != null && !selectedItemIds.Contains(e.ItemId))
                .ToList();
            if (toRemove.Count > 0)
                dbContext.Set<CostCategoryItem>().RemoveRange(toRemove);

            var toAdd = selectedCostCategory.Items?
                .Where(e => !existingsItemIds.Contains(e.Id))
                .Select(e => new CostCategoryItem { CategoryId = selectedCostCategory.Id, ItemId = e.Id })
                .ToList();
            if (toAdd?.Count > 0)
                await dbContext.Set<CostCategoryItem>().AddRangeAsync(toAdd, ct);
        }

        public async Task<IReadOnlyList<CostCategoryItem>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CostCategoryItem>().AsNoTracking()
                .ToListAsync(cancellationToken: ct);

            return result;
        }
    }
}
