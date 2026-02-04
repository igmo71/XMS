using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Application
{
    public class CostCategoryItemService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICostCategoryItemService
    {
        public async Task UpdateByCategoryAsync(CostCategory value, CancellationToken ct = default)
        {
            if (value.Items is null)
                return;

            using var dbContext = dbFactory.CreateDbContext();

            var existingValues = dbContext.CostCategoryItems
                .Where(x => x.CategoryId == value.Id)
                .ToList();

            // Remove
            var selectedItemIds = value.Items.Select(e => e.Id).ToHashSet();

            var existingValuesToRemove = existingValues
                .Where(e => selectedItemIds != null && !selectedItemIds.Contains(e.ItemId));

            dbContext.CostCategoryItems.RemoveRange(existingValuesToRemove);

            // Add
            var existingsItemIds = existingValues.Select(e => e.ItemId).ToHashSet();

            var selectedValuesToAdd = value.Items
                .Where(e => !existingsItemIds.Contains(e.Id))
                .Select(e => new CostCategoryItem { CategoryId = value.Id, ItemId = e.Id });

            dbContext.CostCategoryItems.AddRange(selectedValuesToAdd);

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
