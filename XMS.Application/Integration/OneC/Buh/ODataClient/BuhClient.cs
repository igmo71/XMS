using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace XMS.Application.Integration.OneC.Buh.ODataClient;

internal class BuhClient(HttpClient httpClient, IOptions<BuhClientConfig> options, ILogger<BuhClient> logger)
    : ODataClient<BuhClientConfig>(httpClient, options, logger);
