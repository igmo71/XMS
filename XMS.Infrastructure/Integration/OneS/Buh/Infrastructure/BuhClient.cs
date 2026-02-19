using Microsoft.Extensions.Options;

namespace XMS.Infrastructure.Integration.OneS.Buh.Infrastructure
{
    internal class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options)
        : OneSClient<BuhClientConfig>(httpClient, options);
}
