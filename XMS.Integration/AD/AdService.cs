using XMS.Core.Abstractions.IntegrationServices;
using XMS.Domain.Models;

namespace XMS.Integration.AD;

internal class AdService(AdClient client) : IAdService
{
    public async Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default)
    {
        var result = await client.GetUsersAsync(ct);

        return result ?? [];
    }
}
