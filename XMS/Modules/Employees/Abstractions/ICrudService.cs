namespace XMS.Modules.Employees.Abstractions
{
    public interface ICrudService<TEntity>
    {
        Task CreateAsync(TEntity item, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetListAsync(CancellationToken ct = default);
        Task UpdateAsync(TEntity item, CancellationToken ct = default);
    }
}
