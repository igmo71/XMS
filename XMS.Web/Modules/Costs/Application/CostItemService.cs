using Microsoft.EntityFrameworkCore;
using XMS.Web.Core;
using XMS.Web.Data;
using XMS.Web.Modules.Costs.Abstractions;
using XMS.Web.Modules.Costs.Domain;

namespace XMS.Web.Modules.Costs.Application
{
    public class CostItemService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICostItemService
    {
        public async Task CreateAsync(CostItem item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.CostItems.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.CostItems.FindAsync([id], cancellationToken: ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Категория Затрат не найдена ({id})");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;
            //existing.DeletedBy = 

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
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
