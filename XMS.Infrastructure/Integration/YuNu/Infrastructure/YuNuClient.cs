using Microsoft.Extensions.Options;
using System.Text.Json;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Integration.YuNu.Infrastructure
{
    internal class YuNuClient(HttpClient httpClient, IOptions<YuNuClientConfig> options)
    {
        private readonly YuNuClientConfig clientConfig = options.Value;

        public async Task<IReadOnlyList<YuNuArticleRelation>> GetArticleRelationsAsync(CancellationToken ct = default)
        {
            using var response = await httpClient.GetAsync(clientConfig.ArticleRelations, ct);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(ct);
            using var document = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

            var relations = TryExtractRelations(document.RootElement);
            if (relations is not null)
            {
                return relations;
            }

            return [];
        }

        private static IReadOnlyList<YuNuArticleRelation>? TryExtractRelations(JsonElement root)
        {
            if (root.ValueKind == JsonValueKind.Array)
            {
                return Deserialize(root);
            }

            if (root.ValueKind != JsonValueKind.Object)
            {
                return null;
            }

            foreach (var propertyName in new[] { "data", "items", "results" })
            {
                if (root.TryGetProperty(propertyName, out var nested) && nested.ValueKind == JsonValueKind.Array)
                {
                    return Deserialize(nested);
                }
            }

            return null;
        }

        private static IReadOnlyList<YuNuArticleRelation> Deserialize(JsonElement element)
        {
            return element.Deserialize<List<YuNuArticleRelation>>() ?? [];
        }
    }
}
