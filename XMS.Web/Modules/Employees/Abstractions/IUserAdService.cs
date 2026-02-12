using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Abstractions
{
    public interface IUserAdService
    {
        Task<IReadOnlyList<UserAd>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<UserAd>> LoadListAsync(CancellationToken ct = default);
        Task SaveListAsync(IReadOnlyList<UserAd> list, CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
