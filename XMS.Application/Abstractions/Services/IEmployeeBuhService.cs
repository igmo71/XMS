using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IEmployeeBuhService
    {
        Task<IReadOnlyList<EmployeeBuh>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<EmployeeBuh>> LoadListAsync(CancellationToken ct = default);
        Task SaveListAsync(IReadOnlyList<EmployeeBuh> list, CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
