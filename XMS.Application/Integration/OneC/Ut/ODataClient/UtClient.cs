using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Application.Integration.OneC.Ut.ODataClient;

internal class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options, ILogger<UtClient> logger)
    : ODataClient<UtClientConfig>(httpClient, options, logger)
{ }
