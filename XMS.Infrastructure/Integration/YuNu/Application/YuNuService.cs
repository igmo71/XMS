using Microsoft.Extensions.Options;
using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.YuNu.Infrastructure;

namespace XMS.Infrastructure.Integration.YuNu.Application
{
    internal class YuNuService(YuNuClient client, IOptions<YuNuClientConfig> options) : IYuNuService
    {
        private readonly YuNuClientConfig _yunuConfig = options.Value;
        public async Task<IReadOnlyList<YuNuArticleRelation>?> GetArticleRelationsAsync(CancellationToken ct = default)
        {
            List<YuNuArticleRelation> result = [];

            foreach (var key in _yunuConfig.ApiKeys)
            {
                var relation = await client.GetArticleRelationsAsync(key.Name, ct);

                if (relation != null)
                    result.Add(relation);
            }
            return result;
        }

        public Task<YuNuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default)
        {
            return client.GetArticleRelationsAsync(apiKeyName, ct);
        }
    }
}
