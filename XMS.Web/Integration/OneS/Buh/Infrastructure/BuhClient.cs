using Microsoft.Extensions.Options;

namespace XMS.Web.Integration.OneS.Buh.Infrastructure
{
    public class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options)
        : OneSClient<BuhClientConfig>(httpClient, options);
}
