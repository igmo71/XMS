using XMS.Domain.Models;

namespace XMS.Core.Abstractions.IntegrationServices;

public interface IOneCBuhService
{
    Task<IReadOnlyList<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default);
}
