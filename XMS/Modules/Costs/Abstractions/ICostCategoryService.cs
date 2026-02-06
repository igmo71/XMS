using XMS.Modules.Costs.Domain;
using XMS.Modules.Employees.Abstractions;

namespace XMS.Modules.Costs.Abstractions
{
    public interface ICostCategoryService : ICrudService<CostCategory>
    {
        Task<IReadOnlyList<CostCategory>> GetListIncludingNavigationPropertiesAsync(CancellationToken ct = default);
    }
}
