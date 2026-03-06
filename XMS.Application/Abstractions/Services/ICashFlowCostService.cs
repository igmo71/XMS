using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface ICashFlowCostService
    {
        Task AddCashFlowCostRangeAsync(List<CashFlowCost> items, CancellationToken token);
        Task DeleteCashFlowCostAsync(Guid itemId, CancellationToken token);
        Task<IReadOnlyList<CashFlowCost>> GetListAsync(bool includeDeleted, CancellationToken token);
    }
}
