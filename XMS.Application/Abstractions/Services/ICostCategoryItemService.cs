using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface ICostCategoryItemService
    {
        Task UpdateByCategoryAsync(CostCategory costCategory, IApplicationDbContext? masterDbContext, CancellationToken ct = default);
        Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default);
        Task<IReadOnlyList<CostCategoryItem>> GetListAsync(bool hasCashFlowOnly = true, CancellationToken ct = default);
        Task DeleteCashFlowItemLinkAsync(CostCategoryItem args, CancellationToken ct = default);
        Task AddCashFlowItemLinkAsync(CostCategoryItem args, CancellationToken ct = default);
    }
}
