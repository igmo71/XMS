using XMS.Domain.Models;

namespace XMS.Core.Abstractions.IntegrationServices;

public interface IOneCZupService
{
    Task<IReadOnlyList<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default);
}
