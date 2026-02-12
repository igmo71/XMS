using Microsoft.Extensions.Options;

namespace XMS.Web.Integration.OneS.Zup.Infrastructure
{
    public class ZupClient(HttpClient httpClient, IOptions<ZupClientConfig> options)
        : OneSClient<ZupClientConfig>(httpClient, options);
}
