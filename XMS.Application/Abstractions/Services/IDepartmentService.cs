using XMS.Application.Common;
using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IDepartmentService : ICrudService<Department>
    {
        Task<IReadOnlyList<Department>> GetListAsync(bool ignoreQueryFilters = false, CancellationToken ct = default);
        Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default);
    }
}
