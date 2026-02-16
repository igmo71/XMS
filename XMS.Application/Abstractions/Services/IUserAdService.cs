using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface IUserAdService
    {
        Task<IReadOnlyList<UserAd>> GetListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<UserAd>> LoadListAsync(CancellationToken ct = default);
        Task ReloadListAsync(CancellationToken ct = default);
    }
}
