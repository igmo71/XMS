using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class CashFlowCostService(IDbContextFactoryProxy dbFactory) : ICashFlowCostService
    {
        public async Task AddCashFlowCostRangeAsync(List<CashFlowCost> items, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await dbContext.Set<CashFlowCost>().AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public Task DeleteCashFlowCostAsync(Guid itemId, CancellationToken ct)
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
