using Microsoft.Extensions.Options;

namespace XMS.Web.Integration.OneS
{
    public abstract class OneSClient<TConfig>(HttpClient httpClient, IOptions<TConfig> options)
        where TConfig : OneSClientConfig
    {
        protected readonly TConfig _clientConfig = options.Value;
        public Task<TValue?> GetValueAsync<TValue>(string? uri, CancellationToken ct = default)
        {
            return httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}", ct);
        }
    }
}
