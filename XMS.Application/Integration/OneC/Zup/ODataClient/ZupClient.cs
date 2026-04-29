using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Integrations.OneC.Common;

namespace XMS.Integrations.OneC.Zup.ODataClient;

internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options, ILogger<ZupClient> logger)
    : ODataClient<ZupClientConfig>(httpClient, options, logger);
