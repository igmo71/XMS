using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Integration.YuNu.Infrastructure
{
    internal class YuNuClient(HttpClient httpClient, IOptions<YuNuClientConfig> options)
    {
        private readonly YuNuClientConfig clientConfig = options.Value;

        public async Task<YuNuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, clientConfig.ArticleRelations);

            var apiKey = clientConfig.ApiKeys.FirstOrDefault(e => e.Name.Equals(apiKeyName))
                ?? throw new InvalidOperationException($"ApiKey {apiKeyName} not found.");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey.Value);

            using var response = await httpClient.SendAsync(request, ct);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var relation = JsonSerializer.Deserialize<YuNuArticleRelation>(content);

            return relation;
        }
    }
}
