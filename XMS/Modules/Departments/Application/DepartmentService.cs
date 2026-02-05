using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            await dbContext.Departments
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
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

        public async Task UpdateAsync(Department item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var existing = await dbContext.Departments.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"Department with ID {item.Id} not found");
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
