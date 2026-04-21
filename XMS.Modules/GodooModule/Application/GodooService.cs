using Microsoft.Extensions.Logging;
using XMS.Application.Common;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Application.Mapping;
using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Application;

internal class GodooService(
    IGodooBuhService godooOneSBuhService,
    IYunuService yunuService,
    ILogger<GodooService> logger) : BaseService, IGodooService
{
    public async Task Reload(string apiKeyName, CancellationToken ct)
    {
        using var activity = StartActivity();

        var yunuApiKey = yunuService.GetApiKey(apiKeyName);

        var yunuArticleRelation = await yunuService.GetArticleRelationsAsync(apiKeyName, ct);

        if (yunuArticleRelation?.Products == null)
        {
            if (logger.IsEnabled(LogLevel.Error))
                logger.LogError("{Source} Failed to load YunuArticleRelation", nameof(Reload));
            return;
        }

        foreach (var yunuProduct in yunuArticleRelation.Products)
        {
            Catalog_Номенклатура? oneSProduct = await GetOrCreateProduct(yunuProduct, ct);

            if (oneSProduct is null) continue;

            if (yunuProduct.MarketplaceRelations?.Length > 0)
            {
                foreach (var yunuRelation in yunuProduct.MarketplaceRelations)
                {
                    var yunuProductId = ProductIdMap.From(yunuRelation);

                    if (string.IsNullOrEmpty(yunuProductId))
                    {
                        if (logger.IsEnabled(LogLevel.Error))
                            logger.LogError("OfferId or VendorCode Not Found {yunuProduct}", yunuProduct);
                    }

                    await godooOneSBuhService.CreateInformationRegister_НоменклатураКонтрагентовБЭД(
                        oneSProduct, yunuProduct, yunuRelation, ct);

                    GodooMarketplaceRelation godooMarketplaceRelation = new(
                        YunuProductId: yunuProductId,
                        Marketplace: MarketplaceMap.FromYunu[yunuRelation.Marketplace ?? string.Empty],
                        Barcode: yunuRelation.Barcode ?? string.Empty,
                        OneSProductKey: oneSProduct.Ref_Key,
                        CompanyKey: yunuApiKey.CompanyId);

                    await CreateMarketplaceRelationIfNotExists(godooMarketplaceRelation, ct);
                }
            }
        }
    }


    private async Task<Catalog_Номенклатура?> GetOrCreateProduct(YunuProduct yunuProduct, CancellationToken ct)
    {
        var products = await godooOneSBuhService.GetProductListAsync(yunuProduct.ProductId.ToString(), ct);

        if (products.Count == 0)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} Product Not Exists", nameof(GetOrCreateProduct));
            return await godooOneSBuhService.CreateProductAsync(yunuProduct, ct);
        }
        else if (products.Count == 1)
        {
            var product = products[0];
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} Product Exists {@Product}", nameof(GetOrCreateProduct), product);
            return product;
        }
        else if (products.Count > 1)
        {
            var product = products[0];
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning("{Source} Products Count greater than 1. Take first. {@Products} {@Product}",
                    nameof(GetOrCreateProduct), products, product);
            return product;
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Error))
                logger.LogError("{Source} Can't find or create Product", nameof(GetOrCreateProduct));
            return null;
        }
    }

    private async Task CreateMarketplaceRelationIfNotExists(GodooMarketplaceRelation godooMarketplaceRelation, CancellationToken ct)
    {
        var marketplaceRelation = await godooOneSBuhService.GetMarketplaceRelationListAsync(godooMarketplaceRelation, ct);

        if (marketplaceRelation.Count == 0)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} Not Exists {@GodooMarketplaceRelation}", nameof(CreateMarketplaceRelationIfNotExists), godooMarketplaceRelation);

            await godooOneSBuhService.CreateMarketplaceRelationAsync(godooMarketplaceRelation, ct);
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} Exists {@GodooMarketplaceRelation}", nameof(CreateMarketplaceRelationIfNotExists), godooMarketplaceRelation);
        }
    }
}
