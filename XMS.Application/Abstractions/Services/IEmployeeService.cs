using XMS.Application.Common;
using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services;

public interface IEmployeeService : ICrudService<Employee>
{
    Task<Employee?> GetByAdLoginAsync(string login, CancellationToken ct = default);
    Task<Employee?> GetByUtRefKeyAsync(string refKey, CancellationToken ct = default);
    Task<ServiceResult<IReadOnlyList<Employee>>> GetEmployeesByManagerIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<IReadOnlyList<Employee>>> GetManagersByEmployeeIdAsync(Guid id, CancellationToken ct = default);
}
