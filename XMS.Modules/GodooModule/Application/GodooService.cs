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

            var yunuArticleRelations = await yunuService.GetArticleRelationsAsync(ct);

            if (yunuArticleRelations is null)
                return;

            foreach (var yunuArticleRelation in yunuArticleRelations)
            {
                if (yunuArticleRelation.Value.Result is null)
                {
                    logger.LogError("{Source} YuNuArticleRelation Result is null", nameof(Reload));
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
                                && e.Marketplace == MarketplaceMap.FromYuNu[yunuRelation.Marketplace]
                                && (e.MarketplaceSku == yunuRelation.Sku || e.MarketplaceSku == yunuRelation.NmId)
                                && e.Barcode == yunuRelation.Barcode
                                && e.ProductId == product.Id.ToString()
                                && e.CompanyId == yunuArticleRelation.Key))
                            {
                                logger.LogDebug("{Source} YuNuMarketplaceRelation Not Exists {@YuNuMarketplaceRelation}", nameof(Reload), yunuRelation);
                                await godooOneSBuhService.CreateMarketplaceRelationAsync(product, yunuRelation, yunuArticleRelation.Key, ct);
                            }
                            else
                            {
                                logger.LogDebug("{Source} YuNuMarketplaceRelation Exists {@YuNuMarketplaceRelation}", nameof(Reload), yunuRelation);
                            }
                        }
                    }
                }
            }
        }

        private async Task<Product?> GetProduct(IReadOnlyList<Product> existingProducts, Result yunuProduct, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(yunuProduct.YuNuArticle))
                logger.LogWarning("Try {Source} by YuNuArticle {yunuProduct.YuNuArticle}", nameof(GetProduct), yunuProduct.YuNuArticle);
            else
                logger.LogDebug("Try {Source} by YuNuArticle {yunuProduct.YuNuArticle}", nameof(GetProduct), yunuProduct.YuNuArticle);


            var products = existingProducts.Where(e => e.Sku == yunuProduct.YuNuArticle).ToList();

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

        // TODO: Проходим по всем existingYuNuArticles, смотрим если есть такая Номенклатура, то смотрим marketplaceRelation, при необходимости, добавляем
        // TODO: marketplaceRelation проверяем по yunu_article, {nm_id (wildberries), sku (ozon), ???} -> MarketplaceSku (ИдентификаторТовара),  marketplace, barcode
        // TODO: Маркетплейс в 1C это перечисление! (отправлять строку или число?) Gemini говорит строкой
        // TODO: XMS.Worker
    }
}
