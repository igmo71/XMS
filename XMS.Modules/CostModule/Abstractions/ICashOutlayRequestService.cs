using XMS.Modules.CostModule.Infrastructure.OneS.Models;

namespace XMS.Modules.CostModule.Abstractions
{
    internal interface ICashOutlayRequestService
    {
        /// <summary>
        /// Get Document_ЗаявкаНаРасходованиеДенежныхСредств By Ref_Key
        /// </summary>
        /// <param name="refKey"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Document_ЗаявкаНаРасходованиеДенежныхСредств?> GetCashOutlayRequestByRefKeyAsync(
            string refKey, CancellationToken ct = default);

        /// <summary>
        /// Get Document_ЗаявкаНаРасходованиеДенежныхСредств By Date
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<Document_ЗаявкаНаРасходованиеДенежныхСредств>?> GetCashOutlayRequestByDateAsync(
            DateTime? begin = null, DateTime? end = null, CancellationToken ct = default);
    }
}
