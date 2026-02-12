using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Abstractions
{
    public interface IUserUtService
    {
        Task<IReadOnlyList<UserUt>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<UserUt>> LoadListAsync(CancellationToken ct = default);
        Task SaveListAsync(IReadOnlyList<UserUt> list,CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
