using XMS.Application.Abstractions;
using XMS.Domain.Models;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostCategoryService : ICrudService<CostCategory>
{
    Task<IReadOnlyList<Employee>> GetManagers(CancellationToken ct = default);
}
