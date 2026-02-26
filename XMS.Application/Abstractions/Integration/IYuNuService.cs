using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IYuNuService
    {
        Task<YuNuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default);

        Task<IReadOnlyList<YuNuArticleRelation>?> GetArticleRelationsAsync(CancellationToken ct = default);
    }
}
