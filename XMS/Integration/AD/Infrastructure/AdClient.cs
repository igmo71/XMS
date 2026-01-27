using Microsoft.Extensions.Options;
using XMS.Integration.AD.Domain;

namespace XMS.Integration.AD.Infrastructure
{
    public class AdClient(HttpClient httpClient, IOptions<AdClientConfig> options)
    {
        private readonly AdClientConfig clientConfig = options.Value;
        public Task<AdUser[]?> GetUsers()
        {
            return httpClient.GetFromJsonAsync<AdUser[]>(clientConfig.AdUsers);
        }
    }
}
