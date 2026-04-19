using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XMS.Application.Integration.OneC.Common;

namespace XMS.Application.Integration.OneC.Ut.ODataClient;

internal class UtClient(HttpClient httpClient, IOptions<UtClientConfig> options, ILogger<UtClient> logger)
    : ODataClient<UtClientConfig>(httpClient, options, logger)
{ }
