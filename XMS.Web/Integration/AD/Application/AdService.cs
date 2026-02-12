using XMS.Web.Integration.AD.Domain;
using XMS.Web.Integration.AD.Infrastructure;

namespace XMS.Web.Integration.AD.Application
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
