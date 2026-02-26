using XMS.Domain.Models.Godoo;

namespace XMS.Application.Abstractions.Integration
{
    public interface IGodooOneSBuhService
    {
        Task<IReadOnlyList<Company>> GetCompanyListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Product>> GetProductListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<MarketplaceRelation>> GetMarketplaceRelationListAsync(CancellationToken ct = default);
        Task<Product?> CreateProductAsync(Result yunuProduct, CancellationToken ct = default);
        Task CreateMarketplaceRelationAsync(Product product, YuNuMarketplaceRelation yunuRelation, string companyId, CancellationToken ct = default);
    }
}
