using Microsoft.Extensions.Options;

namespace XMS.Infrastructure.Integration.OneS.Zup.Infrastructure
{
    internal class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options)
        : OneSClient<ZupClientConfig>(httpClient, options);
}
