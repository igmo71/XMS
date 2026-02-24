using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models;
using XMS.Infrastructure.Integration.YuNu.Infrastructure;

namespace XMS.Infrastructure.Integration.YuNu.Application
{
    internal class YuNuService(YuNuClient client) : IYuNuService
    {
        public Task<IReadOnlyList<YuNuArticleRelation>> GetArticleRelationsAsync(CancellationToken ct = default)
        {
            return client.GetArticleRelationsAsync(ct);
        }
    }
}
