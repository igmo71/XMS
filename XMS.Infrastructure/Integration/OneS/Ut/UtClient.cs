using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Infrastructure.Integration.OneS.Ut
{
    internal class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options, ILogger<UtClient> logger)
        : OneSClient<UtClientConfig>(httpClient, options, logger);
}
