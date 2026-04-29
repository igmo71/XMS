using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Integrations.OneC.Common;

namespace XMS.Integrations.OneC.Ut.ODataClient;

internal class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options, ILogger<UtClient> logger)
    : ODataClient<UtClientConfig>(httpClient, options, logger)
{ }
