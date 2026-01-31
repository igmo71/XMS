using Microsoft.EntityFrameworkCore;
using XMS.Data;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class EmployeeService(IDbContextFactory<ApplicationDbContext> dbFactory) : IEmployeeService
    {
        public async Task CreateAsync(Employee item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.Employees.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            await dbContext.Employees
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.Employees.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<Employee>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.Employees
            .AsNoTracking()
            .Include(e => e.City)
            //.Include(e => e.CostItem)
            .Include(e => e.Department)
            .Include(e => e.EmployeeBuh)
            .Include(e => e.EmployeeZup)
            .Include(e => e.JobTitle)
            .Include(e => e.Location)
            .Include(e => e.UserAd)
            .Include(e => e.UserUt)
            //.Include(e => e.LocationManager)
            //.Include(e => e.OperationManager)
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }

        public async Task UpdateAsync(Employee item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var existing = await dbContext.Employees.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"City with ID {item.Id} not found");
            
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            
            existing.EmployeeBuhId = item.EmployeeBuh?.Id;
            existing.EmployeeZupId = item.EmployeeZup?.Id;
            existing.UserUtId = item.UserUt?.Id;
            existing.UserAdId = item.UserAd?.Sid;
            
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
