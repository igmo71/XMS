using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    public class CostCategoryService(
        IDbContextFactoryProxy dbFactory,
        ICostCategoryItemService costCategoryItemService) : ICostCategoryService
    {
        public async Task CreateAsync(CostCategory item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            await costCategoryItemService.UpdateByCategoryAsync(item, dbContext, ct);

            item.ClearCollections();

            dbContext.Set<CostCategory>().Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(CostCategory item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<CostCategory>().FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"CostCategory with ID {item.Id} not found");

            dbContext.UpdateValues(existing, item);

            await costCategoryItemService.UpdateByCategoryAsync(item, dbContext, ct);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<CostCategory>()
                .Include(e => e.Children)
                .Include(e => e.Items)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Категория Затрат не найдена ({id})");

            if (existing.Children.Any(e => !e.IsDeleted))
                return ServiceError.InvalidOperation.WithDescription("Категория Затрат содержит вложенную Категорию");

            if (existing.Items?.Any(e => !e.IsDeleted) == true)
                return ServiceError.InvalidOperation.WithDescription("Категория Затрат содержит вложенную Статью");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<CostCategory>()
                .Include(e => e.Parent)
                .Include(e => e.Items)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Категория Затрат не найдена ({id})");

            if (existing.Parent?.IsDeleted == true)
                return ServiceError.InvalidOperation.WithDescription("Категория Затрат вложена в удаленную Категорию");

            existing.IsDeleted = false;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<CostCategory?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<CostCategory>().FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<CostCategory>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var query = dbContext.Set<CostCategory>()
            .AsNoTracking();

            if (!includeDeleted)
                query = query.Where(e => !e.IsDeleted);

            return await query
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

