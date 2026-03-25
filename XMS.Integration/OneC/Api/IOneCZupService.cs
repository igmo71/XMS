using XMS.Domain.Models;

namespace XMS.Integration.OneC.Api;

public interface IOneCZupService
{
    Task<IReadOnlyList<EmployeeZup>> GetEmployeeListAsync(CancellationToken ct = default);
}
