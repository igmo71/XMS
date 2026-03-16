using XMS.Modules.CostModule.Domain;

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
    }
}
