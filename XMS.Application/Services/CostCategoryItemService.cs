using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    public class CostCategoryItemService(IDbContextFactoryProxy dbFactory) : ICostCategoryItemService
    {
        public async Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CostCategoryItem>()
                .Where(e => e.CategoryId == valueId).
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

        private static async Task ExecuteUpdateLogic(CostCategory costCategory, IApplicationDbContext dbContext, CancellationToken ct)
        {
            var existingValues = await dbContext.Set<CostCategoryItem>()
                .Where(x => x.CategoryId == costCategory.Id)
                .ToListAsync(ct);

            var selectedItemIds = costCategory.Items?.Select(e => e.Id).ToHashSet();
            var existingsItemIds = existingValues.Select(e => e.ItemId).ToHashSet();

            var toRemove = existingValues
                .Where(e => selectedItemIds != null && !selectedItemIds.Contains(e.ItemId))
                .ToList();
            if (toRemove.Count > 0)
                dbContext.Set<CostCategoryItem>().RemoveRange(toRemove);

            var toAdd = costCategory.Items?
                .Where(e => !existingsItemIds.Contains(e.Id))
                .Select(e => new CostCategoryItem { CategoryId = costCategory.Id, ItemId = e.Id })
                .ToList();
            if (toAdd?.Count > 0)
                await dbContext.Set<CostCategoryItem>().AddRangeAsync(toAdd, ct);
        }
    }
}
