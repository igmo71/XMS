using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace XMS.Core.Abstractions.Data;

public interface IApplicationDbContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    void UpdateValues<TEntity>(TEntity existing, TEntity newItem) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken token);

    Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, BulkConfig? bulkConfig = null, CancellationToken ct = default) where TEntity : class;
}
