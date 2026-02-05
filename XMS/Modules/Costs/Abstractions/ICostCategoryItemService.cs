using XMS.Data;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostCategoryItemService
    {
        Task UpdateByCategoryAsync(Guid id, ICollection<CostItem> costItems, ApplicationDbContext? masterDbContext, CancellationToken ct = default);

        Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default);
    }
}
