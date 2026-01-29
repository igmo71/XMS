using XMS.Modules.Employees.Domain;

namespace XMS.Modules.Employees.Abstractions
{
    public interface IJobTitleService
    {
        Task CreateAsync(JobTitle item, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<JobTitle?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<JobTitle>> GetListAsync(CancellationToken ct = default);
        Task UpdateAsync(JobTitle item, CancellationToken ct = default);
    }
}
