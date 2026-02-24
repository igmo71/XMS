using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IYuNuService
    {
        Task<IReadOnlyList<YuNuArticleRelation>> GetArticleRelationsAsync(CancellationToken ct = default);
    }
}
