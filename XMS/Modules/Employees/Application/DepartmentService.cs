using Microsoft.EntityFrameworkCore;
using XMS.Common;
using XMS.Data;
using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Application
{
    public class DepartmentService(IDbContextFactory<ApplicationDbContext> dbFactory) : IDepartmentService
    {
        public async Task CreateAsync(Department item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            dbContext.Departments.Add(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            await dbContext.Departments
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.Departments.FindAsync([id], ct);
        }

        public async Task<IReadOnlyList<Department>> GetListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            return await dbContext.Departments
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
        }

        public async Task UpdateAsync(Department item, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var existing = await dbContext.Departments.FindAsync([item.Id], ct)
                ?? throw new KeyNotFoundException($"Department with ID {item.Id} not found");
            dbContext.Entry(existing).CurrentValues.SetValues(item);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<Department>> GetFlattenedListAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            var list = await GetListAsync(ct);

            var result = TreeHelper.BuildFlattenedTree(list);

            return result;
        }

        public async Task<IReadOnlyList<Department>> GetFullTreeAsync(CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();
            // 1. Загружаем ВСЕ департаменты в память. 
            // EF Core автоматически заполнит свойства Children и Parent, так как он отслеживает зависимости (Identity Resolution).
            var allDepartments = await dbContext.Departments
                .AsNoTracking() // Для скорости, если только просмотр
                .ToListAsync(ct);

            // 2. Возвращаем только корневые элементы (у которых нет ParentId)
            var result =  allDepartments.Where(d => d.ParentId == null).ToList();

            return result;
        }
    }
}
