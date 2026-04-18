using XMS.Domain.Models;

namespace XMS.Core.Abstractions.IntegrationServices;

public interface IAdService
{
    Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default);
}
