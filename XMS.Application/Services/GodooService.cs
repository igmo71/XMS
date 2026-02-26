using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models.Godoo;

namespace XMS.Application.Services
{
    internal class GodooService(IGodooOneSBuhService godooOneSBuhService, IYuNuService yunuService) : IGodooService
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
            var products = await godooOneSBuhService.GetProductListAsync(ct);

            var marketplaceRelation = await godooOneSBuhService.GetMarketplaceRelationListAsync(ct);

            var relationList = await yunuService.GetArticleRelationsAsync(ct);

            if (relationList is null)
                return;

            var yunuArticleRelations = relationList
                .Where(e => e.Result != null)
                .SelectMany(e => e.Result!);

            var existingYuNuArticles = yunuArticleRelations.Select(e => e.YuNuArticle).ToList();

            // TODO: Проходим по всем existingYuNuArticles, смотрим если есть такая Номенклатура, то смотрим marketplaceRelation, при необходимости, добавляем
            // TODO: marketplaceRelation проверяем по yunu_article, {nm_id (wildberries), sku (ozon), ???} -> MarketplaceSku (ИдентификаторТовара),  marketplace, barcode
            // TODO: Маркетплейс в 1C это перечисление! (отправлять строку или число?) Gemini говорит строкой
            // TODO: XMS.Worker
        }
    }
}
