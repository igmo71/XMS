using Microsoft.Extensions.Options;

namespace XMS.Integration.OneS.Ut.Infrastructure
{
    public class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options)
    {
        private readonly UtClientConfig _clientConfig = options.Value;

        public Task<TValue?> GetValue<TValue>(string? uri)
        {
            return httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}");
        }
    }
}
