using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration.Services;

public interface IAdService
{
    Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default);
}
