using XMS.Domain.Models.Godoo;

namespace XMS.Application.Abstractions.Services
{
    public interface IGodooService
    {
        Task<IReadOnlyList<Company>> GetCompanyListAsync(CancellationToken ct);
        Task<IReadOnlyList<Product>> GetProductListAsync(CancellationToken ct);
        Task<IReadOnlyList<MarketplaceRelation>> GetMarketplaceRelationListAsync(CancellationToken ct);
        Task Reload(string apiKeyName, CancellationToken ct);
    }
}
