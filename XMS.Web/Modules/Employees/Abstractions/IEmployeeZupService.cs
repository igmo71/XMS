using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Abstractions
{
    public interface IEmployeeZupService
    {
        Task<IReadOnlyList<EmployeeZup>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<EmployeeZup>> LoadListAsync(CancellationToken ct = default);
        Task SaveListAsync(IReadOnlyList<EmployeeZup> list, CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
