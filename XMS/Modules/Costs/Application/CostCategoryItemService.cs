using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Application
{
    public class CostCategoryItemService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICostCategoryItemService
    {
        public async Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.CostCategoryItems.
                Where(e => e.CategoryId == valueId).
                ExecuteDeleteAsync(ct);
        }

        public async Task UpdateByCategoryAsync(CostCategory costCategory, ApplicationDbContext? masterDbContext, CancellationToken ct = default)
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

        private static async Task ExecuteUpdateLogic(CostCategory costCategory, ApplicationDbContext dbContext, CancellationToken ct)
        {
            var existingValues = await dbContext.CostCategoryItems
                .Where(x => x.CategoryId == costCategory.Id)
                .ToListAsync(ct);

            var selectedItemIds = costCategory.Items?.Select(e => e.Id).ToHashSet();
            var existingsItemIds = existingValues.Select(e => e.ItemId).ToHashSet();

            var toRemove = existingValues
                .Where(e => selectedItemIds != null && !selectedItemIds.Contains(e.ItemId))
                .ToList();
            if (toRemove.Count > 0)
                dbContext.CostCategoryItems.RemoveRange(toRemove);

            var toAdd = costCategory.Items?
                .Where(e => !existingsItemIds.Contains(e.Id))
                .Select(e => new CostCategoryItem { CategoryId = costCategory.Id, ItemId = e.Id })
                .ToList();
            if (toAdd?.Count > 0)
                await dbContext.CostCategoryItems.AddRangeAsync(toAdd, ct);
        }
    }
}
