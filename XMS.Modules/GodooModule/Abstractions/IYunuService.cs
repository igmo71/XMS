using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Abstractions
{
    public interface IYunuService
    {
        Task<YuNuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default);

        Task<Dictionary<string, YuNuArticleRelation>?> GetArticleRelationsAsync(CancellationToken ct = default);
    }
}
