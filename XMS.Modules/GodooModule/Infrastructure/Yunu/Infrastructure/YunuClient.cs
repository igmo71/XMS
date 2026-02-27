using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Infrastructure.Yunu.Infrastructure
{
    internal class YunuClient(HttpClient httpClient, IOptions<YunuClientConfig> options)
    {
        private readonly YunuClientConfig clientConfig = options.Value;

        public async Task<YuNuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, clientConfig.ArticleRelations);

            var apiKey = clientConfig.ApiKeys.FirstOrDefault(e => e.Name.Equals(apiKeyName))
                ?? throw new InvalidOperationException($"ApiKey {apiKeyName} not found.");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey.Value);

            using var response = await httpClient.SendAsync(request, ct);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(ct);

            var relation = JsonSerializer.Deserialize<YuNuArticleRelation>(content);

            return relation;
        }
    }
}
