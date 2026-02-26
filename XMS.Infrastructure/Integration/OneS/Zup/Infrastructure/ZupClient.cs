using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Infrastructure.Integration.OneS.Zup.Infrastructure
{
    internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options, ILogger<ZupClient> logger)
        : OneSClient<ZupClientConfig>(httpClient, options, logger);
}
