using XMS.Modules.Costs.Domain;
using XMS.Modules.Employees.Abstractions;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostCategoryService : ICrudService<CostCategory>
    {
        Task<IReadOnlyList<CostCategory>> GetFlattenedListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<CostCategory>> GetFullListAsync(CancellationToken ct = default);
        Task CreareOrUpdateAsync(CostCategory item, CancellationToken ct);
    }
}
