using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface ICashFlowCostService
    {
        Task AddRangeCashFlowCostAsync(List<CashFlowCost> items, CancellationToken token);
        Task UpdateRangeCashFlowCostAsync(List<CashFlowCost> selectedItems, CancellationToken token);
        Task DeleteCashFlowCostAsync(Guid itemId, CancellationToken token);
        Task<IReadOnlyList<CashFlowCost>> GetListAsync(bool includeDeleted, CancellationToken token);
        Task<HashSet<Guid>> GetSelectedCashFlowItemIds(Guid costCategoryItemId);
    }
}
