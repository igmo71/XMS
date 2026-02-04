using Microsoft.EntityFrameworkCore;
using XMS.Components.Common;
using XMS.Data;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;
using static MudBlazor.CategoryTypes;

namespace XMS.Modules.Costs.Application
{
    public class CostCategoryService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICostCategoryService
    {
        public async Task CreateAsync(CostCategory item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.CostCategories.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            await dbContext.CostCategories
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<CostCategory?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.CostCategories.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<CostCategory>> GetFlattenedListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var list = await GetListAsync(ct);

            var result = TreeHelper.BuildFlattenedTree(list);

            return result;
        }

        public async Task<IReadOnlyList<CostCategory>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.CostCategories
            .AsNoTracking()
            //.Include(e => e.Items)
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<CostCategory>> GetFullListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var list = dbContext.CostCategories
            .AsNoTracking()
            .Include(e => e.Parent)
            .Include(e => e.Children)
            .Include(e => e.Items)
            .OrderBy(x => x.Name)
            .ToList();

            return list;
        }

        public async Task UpdateAsync(CostCategory item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var existing = await dbContext.CostCategories.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"CostCategory with ID {item.Id} not found");
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task CreareOrUpdateAsync(CostCategory item, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = dbContext.CostCategories.FirstOrDefault(e => e.Id == item.Id);

            if(existing is null)
                dbContext.CostCategories.Add(item);
            else
                dbContext.Entry(existing).CurrentValues.SetValues(item);

            await dbContext.SaveChangesAsync(ct);
        }
    }
}

