using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    public class DepartmentService(IDbContextFactoryProxy dbFactory) : IDepartmentService
    {
        public async Task CreateAsync(Department item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            dbContext.Set<Department>().Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Department item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<Department>().FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"Department with ID {item.Id} not found");

            dbContext.UpdateValues(existing, item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<Department>()
                .Include(e => e.Children)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Подразделение не найдено ({id})");

            if (existing.Children.Count > 0)
                return ServiceError.InvalidOperation.WithDescription("Подразделение содержит вложенные Подразделения");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<Department>().IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id, ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Подразделение не найдено ({id})");

            existing.IsDeleted = false;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<Department>().FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<Department>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<Department>()
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Department>> GetListAsync(bool ignoreQueryFilters = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<Department>().AsNoTracking();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return await query.OrderBy(x => x.Name).ToListAsync(ct);
        }
    }
}
