using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IEmployeeZupService
    {
        Task<IReadOnlyList<EmployeeZup>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
        Task<IReadOnlyList<EmployeeZup>> LoadListAsync(CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
