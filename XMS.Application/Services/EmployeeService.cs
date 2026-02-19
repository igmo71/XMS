using Microsoft.EntityFrameworkCore;
using XMS.Application.Abstractions;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;
using XMS.Domain.Models;

namespace XMS.Application.Services
{
    internal class EmployeeService(IDbContextFactoryProxy dbFactory) : IEmployeeService
    {
        public async Task CreateAsync(Employee item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            dbContext.Set<Employee>().Add(item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Employee item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<Employee>().FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"Employee with ID {item.Id} not found");

            dbContext.UpdateValues(existing, item);

            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<Employee>().FindAsync([id], cancellationToken: ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Сотрудник не найден ({id})");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Set<Employee>().FindAsync([id], cancellationToken: ct);

            if (existing is null)
                return ServiceError.NotFound.WithDescription($"Сотрудник не найден ({id})");

            existing.IsDeleted = false;

            await dbContext.SaveChangesAsync(ct);

            return ServiceResult.Success();
        }

        public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<Employee>().FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<Employee>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var query = dbContext.Set<Employee>().AsNoTracking();

            if (!includeDeleted)
                query = query.Where(e => !e.IsDeleted);

            return await query.Include(e => e.City)
                .Include(e => e.Department)
                .Include(e => e.EmployeeBuh)
                .Include(e => e.EmployeeZup)
                .Include(e => e.JobTitle)
                .Include(e => e.Location)
                .Include(e => e.UserAd)
                .Include(e => e.UserUt)
                .Include(e => e.LocationManager)
                .Include(e => e.OperationManager)
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }
    }
}
