using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostCategoryItemService
    {
        Task UpdateByCategoryAsync(CostCategory value, CancellationToken ct = default);
    }
}
