using Microsoft.EntityFrameworkCore;
using XMS.Core;
using XMS.Data;
using XMS.Modules.Departments.Abstractions;
using XMS.Modules.Departments.Domain;

namespace XMS.Modules.Departments.Application
{
    public class DepartmentService(IDbContextFactory<ApplicationDbContext> dbFactory) : IDepartmentService
    {
        public async Task CreateAsync(Department item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            dbContext.Departments.Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Department item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Departments.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"Department with ID {item.Id} not found");

            dbContext.Entry(existing).CurrentValues.SetValues(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Departments
                .Include(e => e.Children)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Подразделение не найдено ({id})");

            if (existing.Children.Count > 0)
                return ServiceError.InvalidOperation.WithDescription("Подразделение содержит вложенные Подразделения");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;
            //existing.DeletedBy = 

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Departments.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id, ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Подразделение не найдено ({id})");

            existing.IsDeleted = false;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Departments.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<Department>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Departments
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Department>> GetListAsync(bool ignoreQueryFilters = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Departments.AsNoTracking();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return await query.OrderBy(x => x.Name).ToListAsync(ct);
        }
    }
}
