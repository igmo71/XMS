using Microsoft.Extensions.Options;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Domain;
using XMS.Modules.GodooModule.Infrastructure.Yunu.Infrastructure;

namespace XMS.Modules.GodooModule.Infrastructure.Yunu.Application
{
    internal class YunuService(YunuClient client, IOptions<YunuClientConfig> options) : IYunuService
    {
        private readonly YunuClientConfig _yunuConfig = options.Value;

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
