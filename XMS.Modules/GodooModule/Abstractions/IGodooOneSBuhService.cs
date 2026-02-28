using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Abstractions
{
    public interface IGodooOneSBuhService
    {
        Task<IReadOnlyList<Catalog_Организации>> GetCompanyListAsync(CancellationToken ct = default);

        Task<IReadOnlyList<Catalog_Номенклатура>> GetProductListAsync (string refKey, CancellationToken ct = default);        
        Task<Catalog_Номенклатура?> CreateProductAsync(YunuProduct yunuProduct, CancellationToken ct = default);

        Task<IReadOnlyList<InformationRegister_НоменклатураМаркетплейсов>> GetMarketplaceRelationListAsync(
            string? yunuProductId,
            string? marketplace,
            string? barcode,
            string? oneSProductKey,
            string? companyKey,
            CancellationToken ct = default);
        
        Task CreateMarketplaceRelationAsync(
            string? yunuProductId,
            string? marketplace,
            string? barcode,
            string? oneSProductKey,
            string? companyKey,
            CancellationToken ct = default);
    }
}
