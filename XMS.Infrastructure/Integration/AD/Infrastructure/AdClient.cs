using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Integration.AD.Infrastructure
{
    public class AdClient(HttpClient httpClient, IOptions<AdClientConfig> options)
    {
        private readonly AdClientConfig clientConfig = options.Value;
        public Task<List<UserAd>?> GetUsersAsync(CancellationToken ct = default)
        {
            return httpClient.GetFromJsonAsync<List<UserAd>>(clientConfig.AdUsers, cancellationToken: ct);
        }
    }
}
