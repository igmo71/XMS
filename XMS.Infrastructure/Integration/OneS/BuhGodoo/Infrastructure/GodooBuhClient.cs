using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Infrastructure.Integration.OneS.BuhGodoo.Infrastructure
{
    internal class GodooBuhClient(HttpClient httpClient, IOptions<GodooBuhClientConfig> options, ILogger<GodooBuhClient> logger)
        : OneSClient<GodooBuhClientConfig>(httpClient, options, logger)
    {
    }
}
