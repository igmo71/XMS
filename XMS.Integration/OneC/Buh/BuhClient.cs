using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Integration.OneC.Buh
{
    internal class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options, ILogger<BuhClient> logger)
        : OneCClient<BuhClientConfig>(httpClient, options, logger);
}
