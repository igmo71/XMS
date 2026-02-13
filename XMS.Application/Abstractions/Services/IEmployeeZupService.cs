using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IEmployeeZupService
    {
        Task<IReadOnlyList<EmployeeZup>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<EmployeeZup>> LoadListAsync(CancellationToken ct = default);
        Task SaveListAsync(IReadOnlyList<EmployeeZup> list, CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
