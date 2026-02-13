using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    public class CityService(IDbContextFactoryProxy dbFactory) : ICityService
    {
        public async Task CreateAsync(City item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            dbContext.Set<City>().Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(City item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<City>().FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"City with ID {item.Id} not found");

            dbContext.UpdateValues(existing, item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<City>().FindAsync([id], cancellationToken: ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Город не найден ({id})");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<City?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<City>().FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<City>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<City>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }
    }
}
