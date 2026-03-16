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
                .Include(e => e.OperationalManager)
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Employee>> GetListAsync(QueryParameters queryParameters, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var result = await dbContext.Set<Employee>()
                .AsNoTracking()
                .HandleQueryParameters(queryParameters)
                .Include(e => e.City)
                .Include(e => e.Department)
                .Include(e => e.EmployeeBuh)
                .Include(e => e.EmployeeZup)
                .Include(e => e.JobTitle)
                .Include(e => e.Location)
                .Include(e => e.UserAd)
                .Include(e => e.UserUt)
                .Include(e => e.LocationManager)
                .Include(e => e.OperationalManager)
                .OrderBy(x => x.Name)
                .ToListAsync(ct);

            return result;
        }

        public async Task<Employee?> GetByAdLoginAsync(string login, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<Employee>().FirstOrDefaultAsync(e => e.UserAd != null && e.UserAd.Login == login, ct);
        }

        public async Task<Employee?> GetByUtRefKeyAsync(string refKey, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            return await dbContext.Set<Employee>().FirstOrDefaultAsync(e => e.UserUtId.ToString() == refKey, ct);
        }

        public async Task<IReadOnlyList<Employee>> GetManagersByEmployeeIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var employees = await dbContext.Set<Employee>().AsNoTracking().ToListAsync(cancellationToken: ct);

            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null) return [];

            var managers = GetAllManagers(employee.Id, employees);

            return managers;
        }

        //private static List<Employee> GetManagers(Employee employee, List<Employee> employees, List<Employee> result)
        //{
        //    var manager = employees.FirstOrDefault(e => e.Id == employee.OperationalManagerId);
        //    if (manager is not null)
        //    {
        //        result.Add(manager);
        //        return GetManagers(manager, employees, result);
        //    }
        //    return result;
        //}


        // DeepSeek //
        public static List<Employee> GetAllManagers(Guid employeeId, List<Employee> employees)
        {
            var employeesById = employees.ToDictionary(e => e.Id);

            var managers = new List<Employee>();
            var visited = new HashSet<Guid>();
            var currentId = employeeId;

            while (employeesById.TryGetValue(currentId, out var current) && current.OperationalManagerId.HasValue)
            {
                var managerId = current.OperationalManagerId.Value;

                // Защита от циклов
                if (!visited.Add(managerId))
                    break;

                if (!employeesById.TryGetValue(managerId, out var manager))
                    break;

                managers.Add(manager);

                currentId = managerId;
            }

            return managers;
        }
        // //

        public async Task<IReadOnlyList<Employee>> GetEmployeesByManagerIdAsync(Guid id, CancellationToken ct = default)
        {
            using var dbContext = dbFactory.CreateDbContext();

            var employees = await dbContext.Set<Employee>().AsNoTracking().ToListAsync(cancellationToken: ct);

            var manager = employees.FirstOrDefault(e => e.Id == id);

            if (manager == null) return [];

            var subordinates = GetAllSubordinatesBFS(manager.Id, employees);

            return subordinates;
        }

        //private static List<Employee> GetSubordinates(Guid managerId, List<Employee> employees, List<Employee> result)
        //{
        //    var subordinates = employees.Where(e => e.OperationalManagerId == managerId).ToList();
        //    if (subordinates.Count == 0)
        //        return [];
        //    foreach (var subordinate in subordinates)
        //    {
        //        result.Add(subordinate);
        //        GetSubordinates(subordinate.Id, employees, result);
        //    }
        //    return result;
        //}


        // Gemini //
        public static List<Employee> GetAllSubordinates(Guid managerId, List<Employee> employees)
        {
            // Создаем быстрый индекс: ManagerId -> Список его людей
            var lookup = employees.ToLookup(e => e.OperationalManagerId);
            var result = new List<Employee>();

            void Traverse(Guid id)
            {
                foreach (var sub in lookup[id])
                {
                    result.Add(sub);
                    Traverse(sub.Id);
                }
            }

            Traverse(managerId);
            return result;
        }

        public static IEnumerable<Employee> GetAllSubordinatesOptimized(Guid managerId, IEnumerable<Employee> allEmployees)
        {
            // Группируем сотрудников по ManagerId один раз (O(n))
            var lookup = allEmployees.ToLookup(e => e.OperationalManagerId);

            // Локальная функция для рекурсивного обхода
            IEnumerable<Employee> Traverse(Guid id)
            {
                foreach (var sub in lookup[id])
                {
                    yield return sub; // Возвращаем прямого подчиненного
                    foreach (var descendant in Traverse(sub.Id))
                    {
                        yield return descendant; // Возвращаем его подчиненных
                    }
                }
            }

            return Traverse(managerId);
        }
        public static IEnumerable<Employee> GetAllSubordinatesLinq(Guid managerId, IEnumerable<Employee> allEmployees)
        {
            // 1. Находим прямых подчиненных для текущего managerId
            var directSubordinates = allEmployees.Where(e => e.OperationalManagerId == managerId).ToList();

            // 2. Для каждого подчиненного рекурсивно ищем его собственных подчиненных
            return directSubordinates.Concat(
                directSubordinates.SelectMany(s => GetAllSubordinatesLinq(s.Id, allEmployees))
            );
        }
        // //

        // DeepSeek
        private static List<Employee> GetAllSubordinatesBFS(Guid managerId, List<Employee> employees)
        {
            // 1. Создаем структуру для быстрого доступа к подчиненным
            var employeesByManager = employees.ToLookup(e => e.OperationalManagerId);

            var result = new List<Employee>();
            var queue = new Queue<Guid>();

            queue.Enqueue(managerId);

            // 2. BFS обход
            while (queue.TryDequeue(out var currentManagerId))
            {
                foreach (var subordinate in employeesByManager[currentManagerId])
                {
                    result.Add(subordinate);
                    queue.Enqueue(subordinate.Id);
                }
            }

            return result;
        }
        // //
    }
}
