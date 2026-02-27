using XMS.Modules.GodooModule.Domain;
using XMS.Modules.GodooModule.Infrastructure.Yunu.Infrastructure;

namespace XMS.Modules.GodooModule.Abstractions
{
    public interface IYunuService
    {
        Task<YunuArticleRelation?> GetArticleRelationsAsync(string apiKeyName, CancellationToken ct = default);

        Task<Dictionary<string, YunuArticleRelation>?> GetArticleRelationsAsync(CancellationToken ct = default);

        ApiKey GetApiKey(string apiKeyName);
    }
}
