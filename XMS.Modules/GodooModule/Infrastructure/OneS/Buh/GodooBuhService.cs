using Microsoft.Extensions.Logging;
using XMS.Application.Common.Integration;
using XMS.Integration.OneC;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Application;
using XMS.Modules.GodooModule.Application.Mapping;
using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh
{
    internal class GodooBuhService(GodooBuhClient buhClient, ILogger<GodooBuhService> logger) : IGodooBuhService
    {

        public async Task<IReadOnlyList<InformationRegister_НоменклатураМаркетплейсов>> GetMarketplaceRelationListAsync(
            GodooMarketplaceRelation godooMarketplaceRelation,
            CancellationToken ct = default)
        {
            string uri = InformationRegister_НоменклатураМаркетплейсов.GetUri(godooMarketplaceRelation);

            var rootObject = await buhClient.GetValueAsync<RootObject<InformationRegister_НоменклатураМаркетплейсов>>(uri, ct);

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

            var relationCreated = await buhClient.PostValueAsync(relationToCreate, nameof(InformationRegister_НоменклатураМаркетплейсов), ct);

            if (relationCreated is null)
                logger.LogError("{Source} - Error {@RelationToCreate}", nameof(CreateMarketplaceRelationAsync), relationToCreate);
            else
                logger.LogInformation("{Source} - Ok {@RelationCreated}", nameof(CreateMarketplaceRelationAsync), relationCreated);
        }

        public async Task<IReadOnlyList<Catalog_Номенклатура>> GetProductListAsync(string yunuProductId, CancellationToken ct = default)
        {
            string uri = Catalog_Номенклатура.GetUri(yunuProductId);

            var rootObject = await buhClient.GetValueAsync<RootObject<Catalog_Номенклатура>>(uri, ct);

            if (rootObject?.Value?.Length == 0)
                logger.LogDebug("{Source} - Not Found by {uri}", nameof(GetProductListAsync), uri);
            else
                logger.LogDebug("{Source} - Found by {uri}", nameof(GetProductListAsync), uri);

            return rootObject?.Value ?? [];
        }

        public async Task<Catalog_Номенклатура?> CreateProductAsync(YunuProduct yunuProduct, CancellationToken ct = default)
        {
            Catalog_Номенклатура newProduct = new()
            {
                Description = $"{yunuProduct.ProductName} ({yunuProduct.ProductId})",
                НаименованиеПолное = $"{yunuProduct.ProductName} ({yunuProduct.ProductId})",
                Артикул = yunuProduct.ProductId.ToString()
            };

            var createdProduct = await buhClient.PostValueAsync(newProduct, nameof(Catalog_Номенклатура), ct);

            if (createdProduct is null)
                logger.LogError("{Source} - Error {@ProductToCreate}", nameof(CreateProductAsync), newProduct);
            else
                logger.LogInformation("{Source} - Ok {@ProductCreated}", nameof(CreateProductAsync), createdProduct);

            return createdProduct;
        }

        public async Task<IReadOnlyList<Catalog_Организации>> GetCompanyListAsync(CancellationToken ct = default)
        {
            var rootObject = await buhClient.GetValueAsync<RootObject<Catalog_Организации>>(Catalog_Организации.Uri, ct);

            return rootObject?.Value ?? [];
        }

        public async Task CreateInformationRegister_НоменклатураКонтрагентовБЭД(
            Catalog_Номенклатура product,
            YunuProduct yunuProduct,
            YunuMarketplaceRelation yunuRelation,
            CancellationToken ct = default)
        {
            var recordToCreate = new InformationRegister_НоменклатураКонтрагентовБЭД
            {
                Номенклатура = product.Ref_Key,
                Наименование = product.Description,
                Артикул = ProductIdMap.From(yunuRelation)?.ToLower(),
                Идентификатор = ProductIdMap.From(yunuRelation),
                Владелец_Key = ProductOwnerKeyMap.From[yunuRelation.Marketplace ?? string.Empty],
                Штрихкод = yunuRelation.Barcode,
                Номенклатура_Type = "StandardODATA.Catalog_Номенклатура"
            };

            var createdRecord = await buhClient.PostValueAsync(recordToCreate, nameof(InformationRegister_НоменклатураКонтрагентовБЭД), ct);

            if (createdRecord is null)
                logger.LogError("{Source} - Error {@RecordToCreate}", nameof(CreateInformationRegister_НоменклатураКонтрагентовБЭД), recordToCreate);
            else
                logger.LogInformation("{Source} - Ok {@CreatedRecord}", nameof(CreateInformationRegister_НоменклатураКонтрагентовБЭД), createdRecord);
        }
    }
}
