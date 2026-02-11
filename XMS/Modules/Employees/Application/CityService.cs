using Microsoft.EntityFrameworkCore;
using XMS.Core;
using XMS.Data;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class CityService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICityService
    {
        public async Task CreateAsync(City item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            dbContext.Cities.Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(City item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext(); 

            var existing = await dbContext.Cities.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"City with ID {item.Id} not found");

            dbContext.Entry(existing).CurrentValues.SetValues(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Cities.FindAsync([id], cancellationToken: ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Город не найден ({id})");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;
            //existing.DeletedBy = 

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<City?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Cities.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<City>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Cities
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }
    }
}
