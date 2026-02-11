using XMS.Core;
using XMS.Core.Abstractions;
using XMS.Modules.Departments.Domain;

namespace XMS.Modules.Departments.Abstractions
{
    public interface IDepartmentService : ICrudService<Department>
    {
        Task<IReadOnlyList<Department>> GetListAsync(bool ignoreQueryFilters = false, CancellationToken ct = default);
        Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default);
    }
}
