using XMS.Modules.Costs.Domain;
using XMS.Modules.Employees.Abstractions;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostCategoryService
    {
        Task CreateOrUpdateAsync(CostCategory value, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<CostCategory>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<CostCategory>> GetFullListAsync(CancellationToken ct = default);
    }
}
