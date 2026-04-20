using XMS.Application.Abstractions.Integration.Services;
using XMS.Domain.Models;

namespace XMS.Application.Integration.AD;

internal class AdService(AdClient client) : IAdService
{
    public async Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default)
    {
        var result = await client.GetUsersAsync(ct);

        return result ?? [];
    }
}
