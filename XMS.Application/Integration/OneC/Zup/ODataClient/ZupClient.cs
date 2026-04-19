using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Application.Integration.OneC.Zup.ODataClient;

internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options, ILogger<ZupClient> logger)
    : ODataClient<ZupClientConfig>(httpClient, options, logger);
