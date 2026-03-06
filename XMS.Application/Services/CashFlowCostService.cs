using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class CashFlowCostService(IDbContextFactoryProxy dbFactory) : ICashFlowCostService
    {
        public Task AddCashFlowCostLinkAsync(CostCategoryItem args, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCashFlowCostLinkAsync(CostCategoryItem args, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<CashFlowCost>> GetListAsync(bool includeDeleted, CancellationToken ct)
        {

            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<CashFlowCost>().AsNoTracking()
                .ToListAsync(cancellationToken: ct);

            return result;
        }
    }
}
