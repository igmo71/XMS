using Microsoft.Extensions.Options;

namespace XMS.Integration.OneS.Ut.Infrastructure
{
    public class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options)
        : OneSClient<UtClientConfig>(httpClient, options);
}
