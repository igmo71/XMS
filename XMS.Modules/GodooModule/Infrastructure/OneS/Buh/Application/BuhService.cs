using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using NetTopologySuite.Operation.Relate;
using XMS.Infrastructure.Integration.OneS;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Application;
using XMS.Modules.GodooModule.Application.Mapping;
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

        public async Task<IReadOnlyList<Catalog_НоменклатураКонтрагентов>> GetProductOfPartnerListAsync(string yunuProductId, CancellationToken ct = default)
        {
            string uri = Catalog_НоменклатураКонтрагентов.GetUri(yunuProductId);

            var rootObject = await client.GetValueAsync<RootObject<Catalog_НоменклатураКонтрагентов>>(uri, ct);

            if (rootObject?.Value?.Length == 0)
                logger.LogDebug("{Source} - Not Found by {uri}", nameof(GetProductOfPartnerListAsync), uri);
            else
                logger.LogDebug("{Source} - Found by {uri}", nameof(GetProductOfPartnerListAsync), uri);

            return rootObject?.Value ?? [];
        }

        public async Task<IReadOnlyList<Catalog_НоменклатураКонтрагентов>?> CreateProductOfPartnerAsync(YunuProduct yunuProduct, CancellationToken ct = default)
        {
            if (yunuProduct.MarketplaceRelations is null || yunuProduct.MarketplaceRelations.Length == 0)
                return null;


            var result = new List<Catalog_НоменклатураКонтрагентов>();

            foreach (var yunuRelation in yunuProduct.MarketplaceRelations)
            {
                if (yunuRelation.Marketplace is null)
                    continue;

                var productOwnerKey = ProductOwnerKeyMap.From[yunuRelation.Marketplace];

                if (productOwnerKey is null)
                    continue;

                Catalog_НоменклатураКонтрагентов newCatalog_НоменклатураКонтрагентовf = new()
                {
                    Description = $"{yunuProduct.ProductName} ({yunuProduct.ProductId})",
                    НаименованиеНоменклатуры = $"{yunuProduct.ProductName} ({yunuProduct.ProductId})",
                    Артикул = yunuProduct.ProductId.ToString()
                };

                var createdCatalog_НоменклатураКонтрагентов = await client.PostValueAsync(newCatalog_НоменклатураКонтрагентовf, nameof(Catalog_НоменклатураКонтрагентов), ct);

                if (createdCatalog_НоменклатураКонтрагентов is null)
                    logger.LogError("{Source} - Error {@ProductToCreate}", nameof(CreateProductOfPartnerAsync), newCatalog_НоменклатураКонтрагентовf);
                else
                {
                    await PatchInformationRegister_НоменклатураКонтрагентовБЭД(createdCatalog_НоменклатураКонтрагентов, yunuProduct, yunuRelation);
                    result.Add(createdCatalog_НоменклатураКонтрагентов);
                    logger.LogInformation("{Source} - Ok {@ProductCreated}", nameof(CreateProductOfPartnerAsync), createdCatalog_НоменклатураКонтрагентов);
                }
            }
            return result;
        }

        private async Task PatchInformationRegister_НоменклатураКонтрагентовБЭД(
            Catalog_НоменклатураКонтрагентов createdCatalog_НоменклатураКонтрагентов,
            YunuProduct yunuProduct,
            YunuMarketplaceRelation yunuRelation,
            CancellationToken ct = default)
        {
            string uri = InformationRegister_НоменклатураКонтрагентовБЭД
                .GetUri(ProductOwnerKeyMap.From[yunuRelation.Marketplace ?? string.Empty], yunuRelation.Sku ?? yunuRelation.NmId);

            var rootObject = await client.GetValueAsync<RootObject<InformationRegister_НоменклатураКонтрагентовБЭД>>(uri, ct);

            InformationRegister_НоменклатураКонтрагентовБЭД? record = rootObject?.Value?.FirstOrDefault();

            var product = await GetProductListAsync(yunuProduct.ProductId.ToString(), ct);

            var recordForPatch = new RecordForPatch
            {

                Номенклатура = product?.FirstOrDefault()?.Ref_Key,
                Номенклатура_Type = "StandardODATA.Catalog_Номенклатура"
            };

            var uriToPatch = $"InformationRegister_НоменклатураКонтрагентовБЭД" +
                $"(Владелец_Key = guid'{record?.Владелец_Key}', Идентификатор = '{record?.Идентификатор}')" +
                $"?$format = json &$inlinecount = allpages";

            var patched = await client.PatchValueAsync(recordForPatch, uriToPatch, ct);
        }

        private class RecordForPatch
        {
            public string? Номенклатура { get; set; }
            public string? Номенклатура_Type { get; set; }
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
                Артикул = product.Артикул,
                Номенклатура_Type = "StandardODATA.Catalog_Номенклатура",
                Владелец_Key = ProductOwnerKeyMap.From[yunuRelation.Marketplace ?? string.Empty],
                //Идентификатор = yunuRelation.Sku ?? yunuRelation.NmId,
                Идентификатор = ProductIdMap.From(yunuRelation),
                Штрихкод = yunuRelation.Barcode
            };

            var createdRecord = await client.PostValueAsync(recordToCreate, nameof(InformationRegister_НоменклатураКонтрагентовБЭД), ct);
        }
    }
}
