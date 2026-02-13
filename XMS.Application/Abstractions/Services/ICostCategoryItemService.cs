using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface ICostCategoryItemService
    {
        Task UpdateByCategoryAsync(CostCategory costCategory, IApplicationDbContext? masterDbContext, CancellationToken ct = default);

        Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default);
    }
}
