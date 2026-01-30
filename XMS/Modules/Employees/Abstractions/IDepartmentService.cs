using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Abstractions
{
    public interface IDepartmentService : ICrudService<Department>
    {
        Task<IReadOnlyList<Department>> GetFlattenedListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Department>> GetFullTreeAsync(CancellationToken ct = default);
    }
}
