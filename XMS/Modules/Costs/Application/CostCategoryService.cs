using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Application
{
    public class CostCategoryService(
        IDbContextFactory<ApplicationDbContext> dbFactory,
        ICostCategoryItemService costCategoryItemService) : ICostCategoryService
    {

        public async Task CreateOrUpdateAsync(CostCategory category, CancellationToken ct)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existingCategory = dbContext.CostCategories.FirstOrDefault(e => e.Id == category.Id);

            Guid? newId = null;

            if (existingCategory is null)
            {
                newId = dbContext.CostCategories.Add(new()
                {
                    Name = category.Name,
                    ParentId = category.ParentId
                }).Entity.Id;
            }
            else
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);


            await costCategoryItemService.UpdateByCategoryAsync(newId ?? category.Id, category.Items ?? [], dbContext, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            await dbContext.CostCategories
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<IReadOnlyList<CostCategory>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.CostCategories
            .AsNoTracking()
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
    }
}

