using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Application.Common.Integration;

namespace XMS.Modules.CostModule.Infrastructure.OneS
{
    internal class CostUtClient(HttpClient httpClient, IOptions<UtClientConfig> options, ILogger<CostUtClient> logger)
        : OneSClient<UtClientConfig>(httpClient, options, logger);
}
