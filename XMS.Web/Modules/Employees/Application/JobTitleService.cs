using Microsoft.EntityFrameworkCore;
using XMS.Web.Core;
using XMS.Web.Data;
using XMS.Web.Modules.Employees.Abstractions;
using XMS.Web.Modules.Employees.Domain;

namespace XMS.Web.Modules.Employees.Application
{
    public class JobTitleService(IDbContextFactory<ApplicationDbContext> dbFactory) : IJobTitleService
    {
        public async Task CreateAsync(JobTitle item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            dbContext.JobTitles.Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(JobTitle item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.JobTitles.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"JobTitle with ID {item.Id} not found");

            dbContext.Entry(existing).CurrentValues.SetValues(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.JobTitles.FindAsync([id], cancellationToken: ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Должность не найдена ({id})");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;
            //existing.DeletedBy = 

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
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
    }
}
