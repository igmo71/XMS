using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions
{
    public interface ICostCatalogUtService
    {
        Task AddRangeAsync(List<CostCatalogUt> items, CancellationToken token);
        Task UpdateRangeAsync(List<CostCatalogUt> selectedItems, CancellationToken token);
        Task DeleteAsync(Guid itemId, CancellationToken token);
        Task<IReadOnlyList<CostCatalogUt>> GetListAsync(CancellationToken token);
        Task<HashSet<Guid>> GetSelectedCostCatalogUtItemIds(Guid costCategoryItemId);
    }
}
