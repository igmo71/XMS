using XMS.Web.Core;
using XMS.Web.Core.Abstractions;
using XMS.Web.Modules.Departments.Domain;

namespace XMS.Web.Modules.Departments.Abstractions
{
    public interface IDepartmentService : ICrudService<Department>
    {
        Task<IReadOnlyList<Department>> GetListAsync(bool ignoreQueryFilters = false, CancellationToken ct = default);
        Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default);
    }
}
