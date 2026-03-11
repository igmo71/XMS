using XMS.Modules.GodooModule.Application;
using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Abstractions
{
    public interface IGodooOneSBuhService
    {
        Task<IReadOnlyList<Catalog_Организации>> GetCompanyListAsync(CancellationToken ct = default);

        Task<IReadOnlyList<Catalog_Номенклатура>> GetProductListAsync(string yunuProductId, CancellationToken ct = default);
        Task<Catalog_Номенклатура?> CreateProductAsync(YunuProduct yunuProduct, CancellationToken ct = default);

        Task<IReadOnlyList<InformationRegister_НоменклатураМаркетплейсов>> GetMarketplaceRelationListAsync(
            GodooMarketplaceRelation godooMarketplaceRelation,
            CancellationToken ct = default);

        Task CreateMarketplaceRelationAsync(GodooMarketplaceRelation godooMarketplaceRelation, CancellationToken ct = default);

        Task CreateInformationRegister_НоменклатураКонтрагентовБЭД(
            Catalog_Номенклатура oneSProduct,
            YunuProduct yunuProduct,
            YunuMarketplaceRelation yunuRelation,
            CancellationToken ct = default);
    }
}
