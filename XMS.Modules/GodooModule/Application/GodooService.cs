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
        public async Task<IReadOnlyList<Company>> GetCompanyListAsync(CancellationToken ct)
        {
            return await godooOneSBuhService.GetCompanyListAsync(ct);
        }

        public async Task<IReadOnlyList<MarketplaceRelation>> GetMarketplaceRelationListAsync(CancellationToken ct)
        {
            return await godooOneSBuhService.GetMarketplaceRelationListAsync(ct);
        }

        public async Task<IReadOnlyList<Product>> GetProductListAsync(CancellationToken ct)
        {
            return await godooOneSBuhService.GetProductListAsync(ct);
        }

        public async Task Reload(string apiKeyName, CancellationToken ct)
        {
            StartActivity();

            var existingProducts = await godooOneSBuhService.GetProductListAsync(ct);

            var marketplaceRelations = await godooOneSBuhService.GetMarketplaceRelationListAsync(ct);

            var yunuApiKey = yunuService.GetApiKey(apiKeyName);

            var yunuArticleRelation = await yunuService.GetArticleRelationsAsync(apiKeyName, ct);            

            if (yunuArticleRelation?.Result == null)
            {
                logger.LogError("{Source} Failed to YunuArticleRelation", nameof(Reload));
                return;
            }

            foreach (var yunuProduct in yunuArticleRelation.Result)
            {
                Product? product = await GetProduct(existingProducts, yunuProduct, ct);

                if (product is null)
                {
                    logger.LogError("{Source} Product is null", nameof(Reload));
                    continue;
                }

                if (yunuProduct.MarketplaceRelations?.Length > 0)
                {
                    foreach (var yunuRelation in yunuProduct.MarketplaceRelations)
                    {
                        if (!marketplaceRelations.Any(e => yunuRelation.Marketplace != null
                            && e.Marketplace == MarketplaceMap.FromYunu[yunuRelation.Marketplace]
                            && e.YunuProductId == yunuProduct.ProductId.ToString()
                            && e.Barcode == yunuRelation.Barcode
                            && e.ProductId == product.Id.ToString()
                            && e.CompanyId == yunuApiKey.CompanyId))
                        {
                            logger.LogDebug("{Source} YunuMarketplaceRelation Not Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
                            await godooOneSBuhService.CreateMarketplaceRelationAsync(product, yunuProduct, yunuRelation, yunuApiKey.CompanyId, ct);
                        }
                        else
                        {
                            logger.LogDebug("{Source} YunuMarketplaceRelation Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
                        }
                    }
                }
            }
        }

        public async Task Reload(CancellationToken ct)
        {
            StartActivity();

            var existingProducts = await godooOneSBuhService.GetProductListAsync(ct);

            var marketplaceRelations = await godooOneSBuhService.GetMarketplaceRelationListAsync(ct);

            var yunuArticleRelations = await yunuService.GetArticleRelationsAsync(ct);

            if (yunuArticleRelations is null)
                return;

            foreach (var yunuArticleRelation in yunuArticleRelations)
            {
                if (yunuArticleRelation.Value.Result is null)
                {
                    logger.LogError("{Source} YunuArticleRelation Result is null", nameof(Reload));
                    continue;
                }

                foreach (var yunuProduct in yunuArticleRelation.Value.Result)
                {
                    Product? product = await GetProduct(existingProducts, yunuProduct, ct);

                    if (product is null)
                    {
                        logger.LogError("{Source} Product is null", nameof(Reload));
                        continue;
                    }

                    if (yunuProduct.MarketplaceRelations?.Length > 0)
                    {
                        foreach (var yunuRelation in yunuProduct.MarketplaceRelations)
                        {
                            if (!marketplaceRelations.Any(e => yunuRelation.Marketplace != null
                                && e.Marketplace == MarketplaceMap.FromYunu[yunuRelation.Marketplace]
                               && e.YunuProductId == yunuProduct.ProductId.ToString()
                                && e.Barcode == yunuRelation.Barcode
                                && e.ProductId == product.Id.ToString()
                                && e.CompanyId == yunuArticleRelation.Key))
                            {
                                logger.LogDebug("{Source} YunuMarketplaceRelation Not Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
                                await godooOneSBuhService.CreateMarketplaceRelationAsync(product, yunuProduct, yunuRelation, yunuArticleRelation.Key, ct);
                            }
                            else
                            {
                                logger.LogDebug("{Source} YunuMarketplaceRelation Exists {@YunuMarketplaceRelation}", nameof(Reload), yunuRelation);
                            }
                        }
                    }
                }
            }
        }

        private async Task<Product?> GetProduct(IReadOnlyList<Product> existingProducts, YunuProduct yunuProduct, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(yunuProduct.YunuArticle))
                logger.LogWarning("Try {Source} by YunuArticle {yunuProduct.YunuArticle}", nameof(GetProduct), yunuProduct.YunuArticle);
            else
                logger.LogDebug("Try {Source} by YunuArticle {yunuProduct.YunuArticle}", nameof(GetProduct), yunuProduct.YunuArticle);


            var products = existingProducts.Where(e => e.Sku == yunuProduct.YunuArticle).ToList();

            if (products.Count == 0)
            {
                logger.LogDebug("{Source} Product Not Exists", nameof(GetProduct));
                return await godooOneSBuhService.CreateProductAsync(yunuProduct, ct);
            }
            else if (products.Count == 1)
            {
                var product = products.First();
                logger.LogDebug("{Source} Product Exists {@Product}", nameof(GetProduct), product);
                return product;
            }
            else if (products.Count > 1)
            {
                var product = products.First();
                logger.LogWarning("{Source} Products Count greater than 1. Take first. {@Products}  {@Product}", nameof(GetProduct), products, product);
                return product;
            }
            else
            {
                logger.LogError("{Source} Can't find or create product", nameof(GetProduct));
                return null;
            }
        }
    }
}
