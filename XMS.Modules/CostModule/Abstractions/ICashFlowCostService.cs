using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions
{
    public interface ICashFlowCostService
    {
        Task AddRangeCashFlowCostAsync(List<CostCatalog_СтатьиДвиженияДенежныхСредств> items, CancellationToken token);
        Task UpdateRangeCashFlowCostAsync(List<CostCatalog_СтатьиДвиженияДенежныхСредств> selectedItems, CancellationToken token);
        Task DeleteCashFlowCostAsync(Guid itemId, CancellationToken token);
        Task<IReadOnlyList<CostCatalog_СтатьиДвиженияДенежныхСредств>> GetListAsync(CancellationToken token);
        Task<HashSet<Guid>> GetSelectedCashFlowItemIds(Guid costCategoryItemId);
    }
}
