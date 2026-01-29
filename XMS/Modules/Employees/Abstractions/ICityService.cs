using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Abstractions
{
    public interface ICityService
    {
        Task CreateAsync(City item, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<City?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<City>> GetListAsync(CancellationToken ct = default);
        Task UpdateAsync(City item, CancellationToken ct = default);
    }
}
