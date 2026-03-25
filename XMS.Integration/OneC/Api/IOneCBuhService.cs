using XMS.Domain.Models;

namespace XMS.Integration.OneC.Api;

public interface IOneCBuhService
{
    Task<IReadOnlyList<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default);
}
