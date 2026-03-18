using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Integration;

namespace XMS.Application.Common.Integration
{
    public class OneSDocumentRepository
    {
        public static async Task DeleteRangeByDateAsync<TEntity>(IApplicationDbContext dbContext, DateTime from, DateTime to, CancellationToken ct) where TEntity : class, IOneSDocument
        {
            await dbContext.Set<TEntity>()
                .Where(e => e.Date >= from && e.Date < to)
                .ExecuteDeleteAsync(cancellationToken: ct);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(IApplicationDbContext dbContext, IReadOnlyList<TEntity> values, CancellationToken ct = default) where TEntity : class
        {
            await dbContext.Set<TEntity>().AddRangeAsync(values, ct);

            return await dbContext.SaveChangesAsync(ct);
        }
    }
}
