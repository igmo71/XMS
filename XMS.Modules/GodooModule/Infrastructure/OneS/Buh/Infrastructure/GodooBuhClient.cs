using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Infrastructure.Integration.OneS;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Infrastructure
{
    internal class GodooBuhClient(HttpClient httpClient, IOptions<GodooBuhClientConfig> options, ILogger<GodooBuhClient> logger)
        : OneSClient<GodooBuhClientConfig>(httpClient, options, logger)
    {
    }
}
