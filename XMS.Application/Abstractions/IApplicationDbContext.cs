using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace XMS.Application.Abstractions
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void UpdateValues<TEntity>(TEntity existing, TEntity newItem) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
