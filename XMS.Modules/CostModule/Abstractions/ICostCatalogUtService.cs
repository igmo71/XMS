using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostCatalogUtService
{
    Task AddRangeAsync(List<CostCatalog_ДДС> items, CancellationToken token);
    Task UpdateRangeAsync(List<CostCatalog_ДДС> selectedItems, CancellationToken token);
    Task DeleteAsync(Guid itemId, CancellationToken token);
    Task<IReadOnlyList<CostCatalog_ДДС>> GetListAsync(CancellationToken token);
    Task<HashSet<Guid>> GetSelectedCostCatalogUtItemIds(Guid costCategoryItemId);
}
