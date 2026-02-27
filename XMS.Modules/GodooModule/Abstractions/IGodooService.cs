using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Abstractions
{
    public interface IGodooService
    {
        Task<IReadOnlyList<Company>> GetCompanyListAsync(CancellationToken ct);
        Task<IReadOnlyList<Product>> GetProductListAsync(CancellationToken ct);
        Task<IReadOnlyList<MarketplaceRelation>> GetMarketplaceRelationListAsync(CancellationToken ct);
        Task Reload(string apiKeyName, CancellationToken ct);
    }
}
