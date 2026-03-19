using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XMS.Core.Abstractions.Data;
using XMS.Domain.Models;

namespace XMS.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //DbContextQueryFilter.ApplyQueryFilter(modelBuilder);
        }

        public void UpdateValues<TEntity>(TEntity existing, TEntity newItem) where TEntity : class =>
            Entry(existing).CurrentValues.SetValues(newItem);

        public Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, BulkConfig? bulkConfig = null, CancellationToken ct = default)
            where TEntity : class =>
            this.BulkInsertAsync(entities, bulkConfig, cancellationToken: ct);
    }
}
