using Microsoft.Extensions.Options;

namespace XMS.Integration.OneS.Ut.Infrastructure
{
    public class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options)
    {
        private readonly UtClientConfig _clientConfig = options.Value;

        public Task<TValue?> GetValueAsync<TValue>(string? uri, CancellationToken ct = default)
        {
            return httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}", ct);
        }
    }
}
