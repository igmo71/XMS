using XMS.Domain.Models;

namespace XMS.Application.Abstractions.Services
{
    public interface ICashFlowCostService
    {
        Task AddCashFlowCostLinkAsync(CostCategoryItem args, CancellationToken token);
        Task DeleteCashFlowCostLinkAsync(CostCategoryItem args, CancellationToken token);
    }
}
