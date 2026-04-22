using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration;

public interface IAdService
{
    Task<UserAd?> GetByLogin(string? login);
    Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default);
}
