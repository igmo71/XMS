using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Application.Common.Integration;
using XMS.Integration.OneC;

namespace XMS.Infrastructure.Integration.OneS.Zup
{
    internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options, ILogger<ZupClient> logger)
        : OneSClient<ZupClientConfig>(httpClient, options, logger);
}
