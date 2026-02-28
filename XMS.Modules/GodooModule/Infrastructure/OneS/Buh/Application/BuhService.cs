using Microsoft.Extensions.Logging;
using XMS.Infrastructure.Integration.OneS;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Application;
using XMS.Modules.GodooModule.Domain;
using XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Infrastructure;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Application
{
    internal class BuhService(GodooBuhClient client, ILogger<BuhService> logger) : IGodooOneSBuhService
    {

        public async Task<IReadOnlyList<InformationRegister_НоменклатураМаркетплейсов>> GetMarketplaceRelationListAsync(
            GodooMarketplaceRelation godooMarketplaceRelation,
            CancellationToken ct = default)
        {
            string uri = InformationRegister_НоменклатураМаркетплейсов.GetUri(godooMarketplaceRelation);

            var rootObject = await client.GetValueAsync<RootObject<InformationRegister_НоменклатураМаркетплейсов>>(uri, ct);

            if (rootObject?.Value?.Length == 0)
                logger.LogDebug("{Source} - Not Found by {uri}", nameof(GetMarketplaceRelationListAsync), uri);
            else
                logger.LogDebug("{Source} - Found by {uri}", nameof(GetMarketplaceRelationListAsync), uri);

            return rootObject?.Value ?? [];
        }

        public async Task CreateMarketplaceRelationAsync(GodooMarketplaceRelation godooMarketplaceRelation,
            CancellationToken ct = default)
        {
            var relationToCreate = new InformationRegister_НоменклатураМаркетплейсов
            {
                ИдентификаторТовара = godooMarketplaceRelation.YunuProductId,
                Маркетплейс = godooMarketplaceRelation.Marketplace,
                Штрихкод = godooMarketplaceRelation.Barcode,
                Номенклатура_Key = godooMarketplaceRelation.OneSProductKey,
                Организация_Key = godooMarketplaceRelation.CompanyKey
            };

            var relationCreated = await client.PostValueAsync(relationToCreate, nameof(InformationRegister_НоменклатураМаркетплейсов), ct);

            if (relationCreated is null)
                logger.LogError("{Source} - Error {@RelationToCreate}", nameof(CreateMarketplaceRelationAsync), relationToCreate);
            else
                logger.LogInformation("{Source} - Ok {@RelationCreated}", nameof(CreateMarketplaceRelationAsync), relationCreated);
        }

        public async Task<IReadOnlyList<Catalog_Номенклатура>> GetProductListAsync(string yunuProductId, CancellationToken ct = default)
        {
            string uri = Catalog_Номенклатура.GetUri(yunuProductId);

            var rootObject = await client.GetValueAsync<RootObject<Catalog_Номенклатура>>(uri, ct);

            if (rootObject?.Value?.Length == 0)
                logger.LogDebug("{Source} - Not Found by {uri}", nameof(GetProductListAsync), uri);
            else
                logger.LogDebug("{Source} - Found by {uri}", nameof(GetProductListAsync), uri);

            return rootObject?.Value ?? [];
        }

        public async Task<Catalog_Номенклатура?> CreateProductAsync(YunuProduct yunuProduct, CancellationToken ct = default)
        {
            Catalog_Номенклатура newCatalog_Номенклатура = new()
            {
                Description = $"{yunuProduct.ProductName} ({yunuProduct.ProductId})",
                НаименованиеПолное = $"{yunuProduct.ProductName} ({yunuProduct.ProductId})",
                Артикул = yunuProduct.ProductId.ToString()
            };

            var createdCatalog_Номенклатура = await client.PostValueAsync(newCatalog_Номенклатура, nameof(Catalog_Номенклатура), ct);

            if (createdCatalog_Номенклатура is null)
                logger.LogError("{Source} - Error {@ProductToCreate}", nameof(CreateProductAsync), newCatalog_Номенклатура);
            else
                logger.LogInformation("{Source} - Ok {@ProductCreated}", nameof(CreateProductAsync), createdCatalog_Номенклатура);

            return createdCatalog_Номенклатура;
        }

        public async Task<IReadOnlyList<Catalog_Организации>> GetCompanyListAsync(CancellationToken ct = default)
        {
            var rootObject = await client.GetValueAsync<RootObject<Catalog_Организации>>(Catalog_Организации.Uri, ct);

            return rootObject?.Value ?? [];
        }
    }
}
