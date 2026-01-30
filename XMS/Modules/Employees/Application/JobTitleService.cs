using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class JobTitleService(IDbContextFactory<ApplicationDbContext> dbFactory) : IJobTitleService
    {
        public async Task CreateAsync(JobTitle item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.JobTitles.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            await dbContext.JobTitles
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<JobTitle?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.JobTitles.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<JobTitle>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.JobTitles
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }

        public async Task UpdateAsync(JobTitle item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var existing = await dbContext.JobTitles.FindAsync([item.Id], ct) 
                ?? throw new KeyNotFoundException($"JobTitle with ID {item.Id} not found");
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
