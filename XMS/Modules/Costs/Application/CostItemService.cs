using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Application
{
    public class CostItemService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICostItemService
    {
        public async Task CreateAsync(CostItem item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.CostItems.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task CreateAsync(CostItem item, ICollection<CostCategory> categories, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.CostItems.Add(item);
            foreach (var category in categories)
                dbContext.Add(new CostCategoryItem { CategoryId = category.Id, ItemId = item.Id });
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            await dbContext.CostItems
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<CostItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.CostItems.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<CostItem>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.CostItems
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }

        public async Task UpdateAsync(CostItem item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var existing = await dbContext.CostItems.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"CostItem with ID {item.Id} not found");
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
