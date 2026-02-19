using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace XMS.Infrastructure.Integration.OneS
{
    internal abstract class OneSClient<TConfig>(HttpClient httpClient, IOptions<TConfig> options)
        where TConfig : OneSClientConfig
    {
        protected readonly TConfig _clientConfig = options.Value;
        public Task<TValue?> GetValueAsync<TValue>(string? uri, CancellationToken ct = default)
        {
            return httpClient.GetFromJsonAsync<TValue>($"{_clientConfig.ServiceUri}/{uri}", ct);
        }
    }
}
