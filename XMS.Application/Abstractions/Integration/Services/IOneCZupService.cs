using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration.Services;

public interface IOneCZupService
{
    Task<IReadOnlyList<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default);
}
