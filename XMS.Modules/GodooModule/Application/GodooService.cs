using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Application
{
    internal class GodooService(
        IGodooOneSBuhService godooOneSBuhService,
        IYunuService yunuService,
        ILogger<GodooService> logger) : BaseService, IGodooService
    {     
        public async Task Reload(string apiKeyName, CancellationToken ct)
        {
            StartActivity();

            //var existingProducts = await godooOneSBuhService.GetProductListAsync(ct);

            //var marketplaceRelations = await godooOneSBuhService.GetMarketplaceRelationListAsync(ct);

            var yunuApiKey = yunuService.GetApiKey(apiKeyName);

            var yunuArticleRelation = await yunuService.GetArticleRelationsAsync(apiKeyName, ct);            

            if (yunuArticleRelation?.Products == null)
            {
                logger.LogError("{Source} Failed to YunuArticleRelation", nameof(Reload));
                return;
            }

            foreach (var yunuProduct in yunuArticleRelation.Products)
            {
                Catalog_Номенклатура? oneSProduct = await GetOrCreateProduct(/*existingProducts,*/ yunuProduct, ct);

                if (oneSProduct is null)
                {
                    logger.LogError("{Source} Catalog_Номенклатура is null", nameof(Reload));
                    continue;
                }

                if (yunuProduct.MarketplaceRelations?.Length > 0)
                {
                    foreach (var yunuRelation in yunuProduct.MarketplaceRelations)
                    {
                        await CreateMarketplaceRelationIfNotExists(/*marketplaceRelations,*/ yunuApiKey, yunuProduct, oneSProduct, yunuRelation, ct);
                    }
                }
            }
        }

        private async Task<Catalog_Номенклатура?> GetOrCreateProduct(/*IReadOnlyList<Catalog_Номенклатура> existingProducts,*/ YunuProduct yunuProduct, CancellationToken ct)
        {
            var existingProducts = await godooOneSBuhService.GetProductListAsync(yunuProduct.ProductId.ToString(), ct);

            var products = existingProducts.Where(e => e.Артикул == yunuProduct.ProductId.ToString()).ToList();

            if (products.Count == 0)
            {
                logger.LogDebug("{Source} Product Not Exists", nameof(GetOrCreateProduct));
                return await godooOneSBuhService.CreateProductAsync(yunuProduct, ct);
            }
            else if (products.Count == 1)
            {
                var product = products.First();
                logger.LogDebug("{Source} Product Exists {@Product}", nameof(GetOrCreateProduct), product);
                return product;
            }
            else if (products.Count > 1)
            {
                var product = products.First();
                logger.LogWarning("{Source} Products Count greater than 1. Take first. {@Products}  {@Product}", nameof(GetOrCreateProduct), products, product);
                return product;
            }
            else
            {
                logger.LogError("{Source} Can't find or create product", nameof(GetOrCreateProduct));
                return null;
            }
        }

        private async Task CreateMarketplaceRelationIfNotExists(/*IReadOnlyList<InformationRegister_НоменклатураМаркетплейсов> marketplaceRelations,*/ Infrastructure.Yunu.Infrastructure.ApiKey yunuApiKey, YunuProduct yunuProduct, Catalog_Номенклатура oneSProduct, YunuMarketplaceRelation yunuRelation, CancellationToken ct)
        {
            var marketplaceRelation = await godooOneSBuhService.GetMarketplaceRelationListAsync(
                yunuProduct.ProductId.ToString(), 
                MarketplaceMap.FromYunu[yunuRelation.Marketplace ?? string.Empty],
                yunuRelation.Barcode,
                oneSProduct.Ref_Key,
                yunuApiKey.CompanyId, ct);

            if (marketplaceRelation is null)
            {
                logger.LogDebug("{Source} YunuMarketplaceRelation Not Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
                await godooOneSBuhService.CreateMarketplaceRelationAsync(oneSProduct, yunuProduct, yunuRelation, yunuApiKey.CompanyId, ct);
            }
            else
            {
                logger.LogDebug("{Source} YunuMarketplaceRelation Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
            }
        }
    }
}
