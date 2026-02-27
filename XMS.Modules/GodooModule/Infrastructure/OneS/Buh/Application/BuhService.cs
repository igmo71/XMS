using Microsoft.Extensions.Logging;
using XMS.Infrastructure.Integration.OneS;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Domain;
using XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Domain;
using XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Infrastructure;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Application
{
    internal class BuhService(GodooBuhClient client, ILogger<BuhService> logger) : IGodooOneSBuhService
    {
        public async Task CreateMarketplaceRelationAsync(Product product, YunuProduct yunuProduct, YunuMarketplaceRelation yunuRelation, string companyId, CancellationToken ct = default)
        {
            if (yunuRelation.Marketplace is null)
            {
                logger.LogError("{Source} YunuMarketplaceRelation Marketplace is null", nameof(CreateMarketplaceRelationAsync));
                return;
            }

            var relationToCreate = new InformationRegister_НоменклатураМаркетплейсов
            {
                Маркетплейс = MarketplaceMap.FromYunu[yunuRelation.Marketplace],
                //ИдентификаторТовара = yunuProduct.YunuArticle ?? string.Empty,
                ИдентификаторТовара = yunuProduct.ProductId.ToString(),
                Штрихкод = yunuRelation.Barcode,
                Номенклатура_Key = product.Id.ToString(),
                Организация_Key = companyId
            };

            var relationCreated = await client.PostValueAsync(relationToCreate, nameof(InformationRegister_НоменклатураМаркетплейсов), ct);

            if (relationCreated is not null)
                logger.LogInformation("{Source} - Ok {@RelationCreated}", nameof(CreateMarketplaceRelationAsync), relationCreated);
            else
                logger.LogError("{Source} - Error {@RelationToCreate}", nameof(CreateMarketplaceRelationAsync), relationToCreate);
        }

        public async Task<Product?> CreateProductAsync(YunuProduct yunuProduct, CancellationToken ct = default)
        {
            Catalog_Номенклатура newCatalog_Номенклатура = new()
            {
                Description = yunuProduct.ProductName,
                НаименованиеПолное = yunuProduct.ProductName,
                Артикул = yunuProduct.YunuArticle
            };

            var createdCatalog_Номенклатура = await client.PostValueAsync(newCatalog_Номенклатура, nameof(Catalog_Номенклатура), ct);

            if (createdCatalog_Номенклатура is null)
            {
                logger.LogInformation("{Source} - Error {@ProductToCreate}", nameof(CreateProductAsync), newCatalog_Номенклатура);

                return null;
            }

            var result = new Product
            {
                Id = createdCatalog_Номенклатура.Ref_Key,
                Name = createdCatalog_Номенклатура.Description,
                Sku = createdCatalog_Номенклатура.Артикул
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
                YunuProductId = e.ИдентификаторТовара,
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
