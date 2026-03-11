using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSUtService
    {
        /// <summary>
        /// Get Catalog_Пользователи
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<UserUt>> GetUserUtListAsync(CancellationToken ct = default);

        /// <summary>
        /// Get Catalog_СтатьиДвиженияДенежныхСредств
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<CashFlowItem>> GetCashFlowItemListAsync(CancellationToken ct = default);

        /// <summary>
        /// Get AccumulationRegister_ТоварыНаСкладах_Balance
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<SkuInventoryUt>> GetStockBalanceUtListAsync(CancellationToken ct = default);
    }
}
