using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Abstractions
{
    public interface IGodooOneSBuhService
    {
        Task<IReadOnlyList<Company>> GetCompanyListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Product>> GetProductListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<MarketplaceRelation>> GetMarketplaceRelationListAsync(CancellationToken ct = default);
        Task<Product?> CreateProductAsync(YunuProduct yunuProduct, CancellationToken ct = default);
        Task CreateMarketplaceRelationAsync(Product product, YunuProduct yunuProduct, YunuMarketplaceRelation yunuRelation, string companyId, CancellationToken ct = default);
    }
}
