using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Domain;
using XMS.Modules.GodooModule.Infrastructure.Yunu.Infrastructure;

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

            var yunuApiKey = yunuService.GetApiKey(apiKeyName);

            var yunuArticleRelation = await yunuService.GetArticleRelationsAsync(apiKeyName, ct);

            if (yunuArticleRelation?.Products == null)
            {
                logger.LogError("{Source} Failed to YunuArticleRelation", nameof(Reload));
                return;
            }

            foreach (var yunuProduct in yunuArticleRelation.Products)
            {
                Catalog_Номенклатура? oneSProduct = await GetOrCreateProduct(yunuProduct, ct);

                if (oneSProduct is null)
                {
                    logger.LogError("{Source} Catalog_Номенклатура is null", nameof(Reload));
                    continue;
                }

                if (yunuProduct.MarketplaceRelations?.Length > 0)
                {
                    foreach (var yunuRelation in yunuProduct.MarketplaceRelations)
                    {
                        await CreateMarketplaceRelationIfNotExists(yunuProduct, yunuRelation, yunuApiKey, oneSProduct, ct);
                    }
                }
            }
        }

        private async Task<Catalog_Номенклатура?> GetOrCreateProduct(YunuProduct yunuProduct, CancellationToken ct)
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

        private async Task CreateMarketplaceRelationIfNotExists(
            YunuProduct yunuProduct,
            YunuMarketplaceRelation yunuRelation,
            ApiKey yunuApiKey,
            Catalog_Номенклатура oneSProduct,
            CancellationToken ct)
        {
            var marketplaceRelation = await godooOneSBuhService.GetMarketplaceRelationListAsync(
                yunuProduct.ProductId.ToString(),
                MarketplaceMap.FromYunu[yunuRelation.Marketplace ?? string.Empty],
                yunuRelation.Barcode,
                yunuApiKey.CompanyId,
                oneSProduct.Ref_Key,
                ct);

            if (marketplaceRelation.Count == 0)
            {
                logger.LogDebug("{Source} YunuMarketplaceRelation Not Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
                await godooOneSBuhService.CreateMarketplaceRelationAsync(yunuProduct, yunuRelation, yunuApiKey.CompanyId, oneSProduct, ct);
            }
            else
            {
                logger.LogDebug("{Source} YunuMarketplaceRelation Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
            }
        }
    }
}
