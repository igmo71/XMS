using Microsoft.Extensions.Options;

namespace XMS.Integration.OneS.Ut.Infrastructure
{
    public class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options)
    {
        private readonly BuhClientConfig _clientConfig = options.Value;

        public Task<TValue?> GetValue<TValue>(string? uri)
        {
            return httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}");
        }
    }
}
