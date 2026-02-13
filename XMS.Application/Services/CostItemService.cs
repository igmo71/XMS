using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    public class CostItemService(IDbContextFactoryProxy dbFactory) : ICostItemService
    {
        public async Task CreateAsync(CostItem item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.Set<CostItem>().Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<CostItem>().FindAsync([id], cancellationToken: ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Категория Затрат не найдена ({id})");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<CostItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.Set<CostItem>().FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<CostItem>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.Set<CostItem>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }

        public async Task UpdateAsync(CostItem item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var existing = await dbContext.Set<CostItem>().FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"CostItem with ID {item.Id} not found");
            dbContext.UpdateValues(existing, item);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
