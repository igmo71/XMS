namespace XMS.Integration.OneC.Ut
{
    internal class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options, ILogger<UtClient> logger)
        : OneCClient<UtClientConfig>(httpClient, options, logger);
}
