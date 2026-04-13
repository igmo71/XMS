using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Integration.OneC.Common;

namespace XMS.Integration.OneC.Zup.ODataClient;

internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options, ILogger<ZupClient> logger)
    : ODataClient<ZupClientConfig>(httpClient, options, logger);
