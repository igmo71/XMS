using XMS.Domain.Models;

namespace XMS.Integration.AD
{
    public interface IAdService
    {
        Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default);
    }
}
