using XMS.Domain.Models.Godoo;

namespace XMS.Application.Abstractions.Integration
{
    public interface IYuNuService
    {
        Task<YuNuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default);

        Task<Dictionary<string, YuNuArticleRelation>?> GetArticleRelationsAsync(CancellationToken ct = default);
    }
}
