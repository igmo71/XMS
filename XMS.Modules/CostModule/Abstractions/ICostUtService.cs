using XMS.Modules.CostModule.Domain;
using XMS.Modules.CostModule.Domain.OneS;

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
        /// Get Document_СписаниеБезналичныхДенежныхСредств By Ref_Key
        /// </summary>
        /// <param name="refKey"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Document_СписаниеБезналичныхДенежныхСредств?> GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKeyAsync(string refKey, CancellationToken ct = default);

        /// <summary>
        /// Get Document_СписаниеБезналичныхДенежныхСредств By Date
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>?> GetDocument_СписаниеБезналичныхДенежныхСредств_ByDateAsync(DateTime date, CancellationToken ct = default);
    }
}
