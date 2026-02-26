using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration;
using XMS.Domain.Models.Godoo;
using XMS.Infrastructure.Integration.OneS.BuhGodoo.Domain;
using XMS.Infrastructure.Integration.OneS.BuhGodoo.Infrastructure;

namespace XMS.Infrastructure.Integration.OneS.BuhGodoo.Application
{
    internal class GodooBuhService(GodooBuhClient client, ILogger<GodooBuhService> logger) : IGodooOneSBuhService
    {
        public async Task CreateMarketplaceRelationAsync(Product product, YuNuMarketplaceRelation yunuRelation, string companyId, CancellationToken ct = default)
        {
            if (yunuRelation.Marketplace is null)
            {
                logger.LogError("{Source} YuNuMarketplaceRelation Marketplace is null", nameof(CreateMarketplaceRelationAsync));
                return;
            }

            if (string.IsNullOrEmpty(yunuRelation.Sku) && string.IsNullOrEmpty(yunuRelation.NmId))
            {
                logger.LogError("{Source} YuNuMarketplaceRelation Sku and NmId is null", nameof(CreateMarketplaceRelationAsync));
                return;
            }

            string marketplaceSku = (yunuRelation.Sku ?? yunuRelation.NmId)!;

            var relation = new InformationRegister_НоменклатураМаркетплейсов
            {
                Маркетплейс = MarketplaceMap.FromYuNu[yunuRelation.Marketplace],
                ИдентификаторТовара = marketplaceSku,
                Штрихкод = yunuRelation.Barcode,
                Номенклатура_Key = product.Id.ToString(),
                Организация_Key = companyId
            };

            var newMarketplaceRelation = await client.PostValueAsync(relation, nameof(InformationRegister_НоменклатураМаркетплейсов), ct);

            logger.LogInformation("{Source} - Ok {@НоменклатураМаркетплейсов}", nameof(CreateMarketplaceRelationAsync), newMarketplaceRelation);
        }

        public async Task<Product?> CreateProductAsync(Result yunuProduct, CancellationToken ct = default)
        {
            Catalog_Номенклатура catalog_Номенклатура = new()
            {
                Description = yunuProduct.ProductName,
                НаименованиеПолное = yunuProduct.ProductName,
                Артикул = yunuProduct.YuNuArticle
            };

            var newCatalog_Номенклатура = await client.PostValueAsync(catalog_Номенклатура, nameof(Catalog_Номенклатура), ct);

            if (newCatalog_Номенклатура is null)
                return null;

            var result = new Product
            {
                Id = newCatalog_Номенклатура.Ref_Key,
                Name = newCatalog_Номенклатура.Description,
                Sku = newCatalog_Номенклатура.Артикул
            };

            logger.LogInformation("{Source} - Ok {@Product}", nameof(CreateProductAsync), result);

            return result;
        }

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
                Barcode = e.Штрихкод,
                ProductId = e.Номенклатура_Key,
                CompanyId = e.Организация_Key
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
                Sku = e.Артикул
            }).ToList();

            return result ?? [];
        }
    }
}
