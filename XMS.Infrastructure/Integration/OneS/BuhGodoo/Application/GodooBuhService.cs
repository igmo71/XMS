using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models.Godoo;
using XMS.Infrastructure.Integration.OneS.BuhGodoo.Domain;
using XMS.Infrastructure.Integration.OneS.BuhGodoo.Infrastructure;

namespace XMS.Infrastructure.Integration.OneS.BuhGodoo.Application
{
    internal class GodooBuhService(GodooBuhClient client) : IGodooOneSBuhService
    {
        public async Task<IReadOnlyList<Company>> GetCompanyListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_Организации>>(Catalog_Организации.Uri, ct);

            var result = rootObject?.Value?.Select(e => new Company
            {
                Id = e.Ref_Key,
                Name = e.Description,
                Code = e.Code
            }).ToList();

            return result ?? [];
        }

        public async Task<IReadOnlyList<MarketplaceRelation>> GetMarketplaceRelationListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<InformationRegister_НоменклатураМаркетплейсов>>(InformationRegister_НоменклатураМаркетплейсов.Uri, ct);

            var result = rootObject?.Value?.Select(e => new MarketplaceRelation
            {
                MarketplaceSku = e.ИдентификаторТовара,
                Marketplace = e.Маркетплейс,
                ProductId = e.Номенклатура_Key,
                CompanyId = e.Организация_Key,
                Barcode = e.Штрихкод
            }).ToList();

            return result ?? [];
        }

        public async Task<IReadOnlyList<Product>> GetProductListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_Номенклатура>>(Catalog_Номенклатура.Uri, ct);

            var result = rootObject?.Value?.Select(e => new Product
            {
                Id = e.Ref_Key,
                Name = e.Description,
                Code = e.Code,
                Sku = e.Артикул,
                IsFolder = e.IsFolder
            }).ToList();

            return result ?? [];
        }
    }
}
