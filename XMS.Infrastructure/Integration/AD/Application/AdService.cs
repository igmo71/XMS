using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.AD.Infrastructure;

namespace XMS.Infrastructure.Integration.AD.Application
{
    public class AdService(AdClient client) : IAdService
    {
        public async Task<List<UserAd>> GetUsersAsync(CancellationToken ct = default)
        {
            var result = await client.GetUsersAsync(ct);

            return result ?? [];
        }
    }
}
