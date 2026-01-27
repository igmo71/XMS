using Microsoft.Extensions.Options;

namespace XMS.Integration.OneS.Zup.Infrastructure
{
    public class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options)
    {
        private readonly ZupClientConfig _clientConfig = options.Value;

        public Task<TValue?> GetValue<TValue>(string? uri)
        {
            return httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}");
        }
    }
}
