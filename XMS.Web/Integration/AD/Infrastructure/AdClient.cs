using Microsoft.Extensions.Options;
using XMS.Web.Integration.AD.Domain;

namespace XMS.Web.Integration.AD.Infrastructure
{
    public class AdClient(HttpClient httpClient, IOptions<AdClientConfig> options)
    {
        private readonly AdClientConfig clientConfig = options.Value;
        public Task<UserAd[]?> GetUsersAsync(CancellationToken ct = default)
        {
            return httpClient.GetFromJsonAsync<UserAd[]>(clientConfig.AdUsers);
        }
    }
}
