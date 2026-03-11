using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Application.Common.Integration;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh
{
    internal class GodooBuhClient(HttpClient httpClient, IOptions<GodooBuhClientConfig> options, ILogger<GodooBuhClient> logger)
        : OneSClient<GodooBuhClientConfig>(httpClient, options, logger)
    {
    }
}
