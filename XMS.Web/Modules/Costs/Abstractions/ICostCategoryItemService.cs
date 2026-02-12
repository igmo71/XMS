using XMS.Web.Data;
using XMS.Web.Modules.Costs.Domain;

namespace XMS.Web.Modules.Costs.Abstractions
{
    public interface ICostCategoryItemService
    {
        Task UpdateByCategoryAsync(CostCategory costCategory, ApplicationDbContext? masterDbContext, CancellationToken ct = default);

        Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default);
    }
}
