using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class CityService(ApplicationDbContext dbContext) : ICityService
    {
        public async Task CreateAsync(City item, CancellationToken ct = default)
        {
            dbContext.Cities.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            await dbContext.Cities
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<City?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            await dbContext.Cities.FindAsync([id], ct);

        public async Task<IReadOnlyList<City>> GetListAsync(CancellationToken ct = default) =>
            await dbContext.Cities
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);

        public async Task UpdateAsync(City item, CancellationToken ct = default)
        {
            var existing = await dbContext.Cities.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"City with ID {item.Id} not found");
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
