using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh;

internal class GodooBuhClient(HttpClient httpClient, IOptions<GodooBuhClientConfig> options, ILogger<GodooBuhClient> logger)
    : ODataClient<GodooBuhClientConfig>(httpClient, options, logger)
{
}
