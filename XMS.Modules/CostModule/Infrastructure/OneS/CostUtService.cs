using XMS.Application.Common.Integration;
using XMS.Integration.OneC;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain;
using XMS.Modules.CostModule.Domain.OneS;

namespace XMS.Modules.CostModule.Infrastructure.OneS
{
    internal class CostUtService(CostUtClient utClient) : ICostUtService
    {
        /// <summary>
        /// Get Catalog_СтатьиДвиженияДенежныхСредств
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<CashFlowItem>> GetCashFlowItemListAsync(CancellationToken ct = default)
        {
            var rootObject = await utClient
                .GetValueAsync<RootObject<Catalog_СтатьиДвиженияДенежныхСредств>>(Catalog_СтатьиДвиженияДенежныхСредств.Uri, ct);

            var result = rootObject?.Value?.Select(x => new CashFlowItem
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                ParentId = x.Parent_Key,
                Code = x.Code,
                IsFolder = x.IsFolder,
                IsDeleted = x.DeletionMark
            }).ToList();

            return result ?? [];
        }

        /// <summary>
        /// Get Document_ЗаявкаНаРасходованиеДенежныхСредств By Date
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>?> GetDocument_СписаниеБезналичныхДенежныхСредств_ByDateAsync(
            DateTime date, CancellationToken ct = default)
        {
            var uri = Document_СписаниеБезналичныхДенежныхСредств.GetUriByDate(date, date.AddDays(1));

            var rootObject = await utClient.GetValueAsync<RootObject<Document_СписаниеБезналичныхДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?.ToList();

            return result;
        }

        /// <summary>
        /// Get Document_ЗаявкаНаРасходованиеДенежныхСредств By Ref_Key
        /// </summary>
        /// <param name="refKey"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Document_СписаниеБезналичныхДенежныхСредств?> GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKeyAsync(
            string refKey, CancellationToken ct = default)
        {
            var uri = Document_СписаниеБезналичныхДенежныхСредств.GetUriByRefKey(refKey);

            var rootObject = await utClient
                .GetValueAsync<RootObject<Document_СписаниеБезналичныхДенежныхСредств>>(uri, ct);

            var result = rootObject?.Value?[0];

            return result;
        }
    }
}
