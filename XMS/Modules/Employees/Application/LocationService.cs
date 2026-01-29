using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class LocationService(ApplicationDbContext dbContext) : ILocationService
    {
        public async Task CreateAsync(Location item, CancellationToken ct = default)
        {
            dbContext.Locations.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            await dbContext.Locations
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<Location?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            await dbContext.Locations.FindAsync([id], ct);

        public async Task<IReadOnlyList<Location>> GetListAsync(CancellationToken ct = default) =>
            await dbContext.Locations
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);

        public async Task UpdateAsync(Location item, CancellationToken ct = default)
        {
            var existing = await dbContext.Locations.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"Post with ID {item.Id} not found");
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
