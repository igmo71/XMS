using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions
{
    public interface ICashFlowCostService
    {
        Task AddRangeCashFlowCostAsync(List<CashFlowCost> items, CancellationToken token);
        Task UpdateRangeCashFlowCostAsync(List<CashFlowCost> selectedItems, CancellationToken token);
        Task DeleteCashFlowCostAsync(Guid itemId, CancellationToken token);
        Task<IReadOnlyList<CashFlowCost>> GetListAsync(CancellationToken token);
        Task<HashSet<Guid>> GetSelectedCashFlowItemIds(Guid costCategoryItemId);
    }
}
