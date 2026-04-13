using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Integration.OneC.Common;

namespace XMS.Integration.OneC.Ut.ODataClient;

internal class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options, ILogger<UtClient> logger)
    : ODataClient<UtClientConfig>(httpClient, options, logger);
