using Microsoft.Extensions.Options;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Infrastructure.Yunu
{
    internal class YunuService(YunuClient client, IOptions<YunuClientConfig> options) : IYunuService
    {
        private readonly YunuClientConfig _yunuConfig = options.Value;

        public YunuClientConfig YunuConfig => _yunuConfig;

        public async Task<Dictionary<string, YunuArticleRelation>?> GetArticleRelationsAsync(CancellationToken ct = default)
        {
            Dictionary<string, YunuArticleRelation> result = [];

            foreach (var apiKey in _yunuConfig.ApiKeys)
            {
                var relation = await client.GetArticleRelationsAsync(apiKey.Name, ct);

                if (relation?.Status == "OK")
                    result.Add(apiKey.CompanyId, relation);
            }
            return result;
        }

        public Task<YunuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default)
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
