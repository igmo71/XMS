using XMS.Integration.AD.Domain;
using XMS.Integration.AD.Infrastructure;

namespace XMS.Integration.AD.Application
{
    public interface IAdService
    {
        Task<UserAd[]> GetUsersAsync(CancellationToken ct = default);
    }

    public class AdService(AdClient client) : IAdService
    {
        public async Task<UserAd[]> GetUsersAsync(CancellationToken ct = default)
        {
            var result = await client.GetUsersAsync(ct);

            return result ?? [];
        }
    }
}
