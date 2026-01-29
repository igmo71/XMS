using Microsoft.Extensions.Options;

namespace XMS.Integration.OneS.Buh.Infrastructure
{
    public class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options)
    {
        private readonly BuhClientConfig _clientConfig = options.Value;

        public Task<TValue?> GetValueAsync<TValue>(string? uri, CancellationToken ct = default)
        {
            return httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}", ct);
        }
    }
}
