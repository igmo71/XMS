using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Integration.AD
{
    internal class AdService(AdClient client) : IAdService
    {
        public async Task<IReadOnlyList<UserAd>> GetUsersAsync(CancellationToken ct = default)
        {
            var result = await client.GetUsersAsync(ct);

            return result ?? [];
        }
    }
}
