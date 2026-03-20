using Microsoft.EntityFrameworkCore;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC;
using XMS.Integration.OneC.Abstractions;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application
{
    internal class CostCatalogUtService(
        IDbContextFactoryProxy dbFactory,
        IOneCUtService utService) : ICostCatalogUtService
    {
        public async Task AddRangeAsync(List<CostCatalogUt> items, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CostCatalogUt>().AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateRangeAsync(List<CostCatalogUt> selectedItems, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existingItems = await dbContext.Set<CostCatalogUt>()
                .Where(e => e.CostCategoryItemId == selectedItems[0].CostCategoryItemId)
                .ToListAsync(ct);

            var existingItemIds = existingItems.Select(e => e.Id);
            var selectedItemIds = selectedItems.Select(e => e.Id);

            var toRemove = existingItems.Where(e => !selectedItemIds.Contains(e.Id)).ToList();
            if (toRemove?.Count > 0)
                dbContext.Set<CostCatalogUt>().RemoveRange(toRemove);

            var toAdd = selectedItems.Where(e => !existingItemIds.Contains(e.Id)).ToList();
            if (toAdd?.Count > 0)
                await dbContext.Set<CostCatalogUt>().AddRangeAsync(toAdd, ct);


            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid itemId, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CostCatalogUt>()
                .Where(x => x.Id == itemId)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<IReadOnlyList<CostCatalogUt>> GetListAsync(CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CostCatalogUt>()
                .AsNoTracking()
                .ToListAsync(ct);

            var catalogItems = (await utService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async(new CatalogQueryParameters(), ct))
                .ToDictionary(e => e.Ref_Key);

            result.ForEach(e => e.CatalogUtItem = catalogItems[e.CatalogUtRefKey]);

            return result;
        }

        public async Task<HashSet<Guid>> GetSelectedCostCatalogUtItemIds(Guid costCategoryItemId)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CostCatalogUt>()
                .AsNoTracking()
                .Where(e => e.CostCategoryItemId == costCategoryItemId)
                .Select(e => e.CatalogUtRefKey)
                .ToHashSetAsync();

            return result;
        }
    }
}
