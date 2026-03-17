using XMS.Modules.CostModule.Domain;
using XMS.Modules.CostModule.Infrastructure.OneS.Models;

namespace XMS.Modules.CostModule.Abstractions
{
    public interface ICostUtService
    {
        /// <summary>
        /// Get Catalog_СтатьиДвиженияДенежныхСредств
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<CashFlowItem>> GetCashFlowItemListAsync(CancellationToken ct = default);

        /// <summary>
        /// Get Document_ЗаявкаНаРасходованиеДенежныхСредств By Ref_Key
        /// </summary>
        /// <param name="refKey"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Document_ЗаявкаНаРасходованиеДенежныхСредств?> GetDocument_ЗаявкаНаРасходованиеДенежныхСредств_ByRefKeyAsync(
            string refKey, CancellationToken ct = default);

        /// <summary>
        /// Get Document_ЗаявкаНаРасходованиеДенежныхСредств By Date
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<Document_ЗаявкаНаРасходованиеДенежныхСредств>?> GetDocument_ЗаявкаНаРасходованиеДенежныхСредств_ByDateAsync(
            DateTime? begin = null, DateTime? end = null, CancellationToken ct = default);
    }
}
