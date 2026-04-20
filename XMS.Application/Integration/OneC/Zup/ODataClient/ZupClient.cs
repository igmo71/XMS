using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Application.Integration.OneC.Common;

namespace XMS.Application.Integration.OneC.Zup.ODataClient;

internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options, ILogger<ZupClient> logger)
    : ODataClient<ZupClientConfig>(httpClient, options, logger);
