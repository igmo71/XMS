using Microsoft.Extensions.Options;
using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models.Godoo;
using XMS.Infrastructure.Integration.YuNu.Infrastructure;

namespace XMS.Infrastructure.Integration.YuNu.Application
{
    internal class YuNuService(YuNuClient client, IOptions<YuNuClientConfig> options) : IYuNuService
    {
        private readonly YuNuClientConfig _yunuConfig = options.Value;

        public async Task<Dictionary<string, YuNuArticleRelation>?> GetArticleRelationsAsync(CancellationToken ct = default)
        {
            Dictionary<string, YuNuArticleRelation> result = [];

            foreach (var apiKey in _yunuConfig.ApiKeys)
            {
                var relation = await client.GetArticleRelationsAsync(apiKey.Name, ct);

                if (relation?.Status == "OK")
                    result.Add(apiKey.CompanyId, relation);
            }
            return result;
        }

        public Task<YuNuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default)
        {
            return client.GetArticleRelationsAsync(apiKeyName, ct);
        }
    }
}
