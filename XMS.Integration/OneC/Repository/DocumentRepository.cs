using Microsoft.EntityFrameworkCore;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Repository
{
    internal class DocumentRepository
    {
        public static async Task DeleteRangeByDateAsync<TEntity>(
            IApplicationDbContext dbContext,
            DateTime from,
            DateTime to,
            CancellationToken ct = default) where TEntity : class, IOneCDocument
        {
            await dbContext.Set<TEntity>()
                .Where(e => e.Date >= from && e.Date < to)
                .ExecuteDeleteAsync(ct);
        }

        public static async Task DeleteByRefKeyAsync<TEntity>(
            IApplicationDbContext dbContext,
            Guid refKey,
            CancellationToken ct = default) where TEntity : class, IOneCDocument
        {
            await dbContext.Set<TEntity>()
                .Where(e => e.Ref_Key == refKey)
                .ExecuteDeleteAsync(ct);
        }

        public static async Task Add<TEntity>(
            IApplicationDbContext dbContext,
            TEntity value,
            CancellationToken ct = default) where TEntity : class, IOneCDocument
        {
            await dbContext.Set<TEntity>().AddAsync(value, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(
            IApplicationDbContext dbContext,
            IReadOnlyList<TEntity> values,
            CancellationToken ct = default) where TEntity : class, IOneCDocument
        {
            await dbContext.Set<TEntity>().AddRangeAsync(values, ct);

            return await dbContext.SaveChangesAsync(ct);
        }
    }
}
