using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IEmployeeBuhService
    {
        Task<IReadOnlyList<EmployeeBuh>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
        Task<IReadOnlyList<EmployeeBuh>> LoadListAsync(CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
