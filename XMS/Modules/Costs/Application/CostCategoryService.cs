using Microsoft.EntityFrameworkCore;
using XMS.Core;
using XMS.Data;
using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Domain;

namespace XMS.Modules.Costs.Application
{
    public class CostCategoryService(
        IDbContextFactory<ApplicationDbContext> dbFactory,
        ICostCategoryItemService costCategoryItemService) : ICostCategoryService
    {
        public async Task CreateAsync(CostCategory item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await costCategoryItemService.UpdateByCategoryAsync(item, dbContext, ct);

            item.ClearCollections();

            dbContext.CostCategories.Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(CostCategory item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.CostCategories.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"CostCategory with ID {item.Id} not found");

            dbContext.Entry(existing).CurrentValues.SetValues(item);

            await costCategoryItemService.UpdateByCategoryAsync(item, dbContext, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.CostCategories
                .Include(e => e.Children)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Категория Затрат не найдена ({id})");

            if (existing.Children.Count > 0)
                return ServiceError.InvalidOperation.WithDescription("Категория Затрат содержит вложенную Категорию");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;
            //existing.DeletedBy = 

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<CostCategory?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.CostCategories.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<CostCategory>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.CostCategories
            .AsNoTracking()
            //.Include(e => e.Parent)
            //.Include(e => e.Children)
            .Include(e => e.Items)
            .Include(e => e.Department)
            .Include(e => e.Employee)
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }
    }
}

