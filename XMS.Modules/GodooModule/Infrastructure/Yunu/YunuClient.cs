using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Infrastructure.Yunu;

internal class YunuClient(HttpClient httpClient, IOptions<YunuClientConfig> options)
{
    private readonly YunuClientConfig clientConfig = options.Value;

    public async Task<YunuArticleRelation?> GetArticleRelationsAsync(string apiKeyValue, CancellationToken ct = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, clientConfig.ArticleRelations);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKeyValue);

        using var response = await httpClient.SendAsync(request, ct);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ct);

        var relation = JsonSerializer.Deserialize<YunuArticleRelation>(content);

        return relation;
    }
}
