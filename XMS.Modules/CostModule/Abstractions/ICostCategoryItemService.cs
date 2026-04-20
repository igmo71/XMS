using XMS.Application.Abstractions.Data;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostCategoryItemService
{
    Task CreateAsync(Guid costCategoryId, Guid CistItemId, CancellationToken ct = default);
    Task UpdateByCategoryAsync(CostCategory costCategory, IApplicationDbContext? masterDbContext, CancellationToken ct = default);
    Task DeleteByCategoryAsync(Guid valueId, CancellationToken ct = default);
    Task<IReadOnlyList<CostCategoryItem>> GetListAsync(CancellationToken ct = default);
}
