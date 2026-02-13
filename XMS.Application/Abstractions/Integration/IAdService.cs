using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IAdService
    {
        Task<List<UserAd>> GetUsersAsync(CancellationToken ct = default);
    }
}
