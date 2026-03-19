using System;
using System.Collections.Generic;
using System.Text;
using XMS.Core.Abstractions.Data;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Repository
{
    internal class CatalogRepository
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
    }
}
