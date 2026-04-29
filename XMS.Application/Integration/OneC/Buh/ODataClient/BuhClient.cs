using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Integrations.OneC.Common;

namespace XMS.Integrations.OneC.Buh.ODataClient;

internal class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options, ILogger<BuhClient> logger)
    : ODataClient<BuhClientConfig>(httpClient, options, logger);
