using Microsoft.Extensions.Options;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Domain;
using XMS.Modules.GodooModule.Infrastructure.Yunu.Infrastructure;

namespace XMS.Modules.GodooModule.Infrastructure.Yunu.Application
{
    internal class YunuService(YunuClient client, IOptions<YunuClientConfig> options) : IYunuService
    {
        private readonly YunuClientConfig _yunuConfig = options.Value;

        public YunuClientConfig YunuConfig => _yunuConfig;

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
            var apiKey = GetApiKey(apiKeyName);

            return client.GetArticleRelationsAsync(apiKey.Value, ct);
        }

        public ApiKey GetApiKey(string apiKeyName)
        {
            var apiKey = _yunuConfig.ApiKeys.FirstOrDefault(e => e.Name.Equals(apiKeyName))
                ?? throw new InvalidOperationException($"ApiKey {apiKeyName} not found.");
            return apiKey;
        }
    }
}
