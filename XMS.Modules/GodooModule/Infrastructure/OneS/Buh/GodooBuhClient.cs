using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Integration.OneC;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh
{
    internal class GodooBuhClient(HttpClient httpClient, IOptions<GodooBuhClientConfig> options, ILogger<GodooBuhClient> logger)
        : OneCClient<GodooBuhClientConfig>(httpClient, options, logger)
    {
    }
}
