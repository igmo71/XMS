using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration.Services;

public interface IOneCBuhService
{
    Task<IReadOnlyList<EmployeeBuh>> GetEmployeeBuhListAsync(CancellationToken ct = default);
}
