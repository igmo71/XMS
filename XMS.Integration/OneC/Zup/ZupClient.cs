using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Integration.OneC.Zup
{
    internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options, ILogger<ZupClient> logger)
        : OneCClient<ZupClientConfig>(httpClient, options, logger);
}
