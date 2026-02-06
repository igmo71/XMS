using XMS.Data;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostCategoryItemService
    {
        Task UpdateByCategoryAsync(CostCategory costCategory, ApplicationDbContext? masterDbContext, CancellationToken ct = default);

        Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default);
    }
}
