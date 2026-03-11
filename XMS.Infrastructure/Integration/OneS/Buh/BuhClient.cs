using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Infrastructure.Integration.OneS.Buh
{
    internal class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options, ILogger<BuhClient> logger)
        : OneSClient<BuhClientConfig>(httpClient, options, logger);
}
