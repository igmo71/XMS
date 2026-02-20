using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSUtService
    {
        Task<IReadOnlyList<UserUt>> GetUserUtListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<CashFlowItem>> GetCashFlowItemListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<SkuInventoryUt>> GetStockBalanceUtListAsync(CancellationToken ct = default);
    }
}
