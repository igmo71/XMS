using Microsoft.EntityFrameworkCore;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Application;

internal class CostCatalogUtService(
    IDbContextFactoryProxy dbFactory,
    IOneCUtService utService) : ICostCatalogUtService
{
    public async Task AddRangeAsync(List<CostCatalog_ДДС> items, CancellationToken ct)
    {
        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<CostCatalog_ДДС>().AddRangeAsync(items, ct);

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateRangeAsync(List<CostCatalog_ДДС> selectedItems, CancellationToken ct)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var existingItems = await dbContext.Set<CostCatalog_ДДС>()
            .Where(e => e.CostCategoryItemId == selectedItems[0].CostCategoryItemId)
            .ToListAsync(ct);

        var existingItemIds = existingItems.Select(e => e.Id);
        var selectedItemIds = selectedItems.Select(e => e.Id);

        var toRemove = existingItems
            .Where(e => !selectedItemIds.Contains(e.Id))
            .ToList();
        if (toRemove?.Count > 0)
            dbContext.Set<CostCatalog_ДДС>().RemoveRange(toRemove);

        var toAdd = selectedItems
            .Where(e => !existingItemIds.Contains(e.Id) && e.Catalog_СтатьяДДС_Key != Guid.Empty)
            .ToList();
        if (toAdd?.Count > 0)
            await dbContext.Set<CostCatalog_ДДС>().AddRangeAsync(toAdd, ct);


        await dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid itemId, CancellationToken ct)
    {
        using var dbContext = dbFactory.CreateDbContext();

        await dbContext.Set<CostCatalog_ДДС>()
            .Where(x => x.Id == itemId)
            .ExecuteDeleteAsync(ct);
    }

    public async Task<IReadOnlyList<CostCatalog_ДДС>> GetListAsync(CancellationToken ct)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var result = await dbContext.Set<CostCatalog_ДДС>()
            .AsNoTracking()
            .ToListAsync(ct);

        var catalogItems = (await utService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async(new CatalogQueryParameters(), ct))
            .ToDictionary(e => e.Ref_Key);

        result.ForEach(e => e.Catalog_СтатьяДДС = catalogItems[e.Catalog_СтатьяДДС_Key]);

        return result;
    }

    public async Task<HashSet<Guid>> GetSelectedCostCatalogUtItemIds(Guid costCategoryItemId)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var result = await dbContext.Set<CostCatalog_ДДС>()
            .AsNoTracking()
            .Where(e => e.CostCategoryItemId == costCategoryItemId)
            .Select(e => e.Catalog_СтатьяДДС_Key)
            .ToHashSetAsync();

        return result;
    }
}
