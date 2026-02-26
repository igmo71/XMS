using Microsoft.Extensions.Options;

namespace XMS.Infrastructure.Integration.OneS.BuhGodoo.Infrastructure
{
    internal class GodooBuhClient(HttpClient httpClient, IOptions<GodooBuhClientConfig> options)
        : OneSClient<GodooBuhClientConfig>(httpClient, options);
}
