using XMS.Application.Common;

namespace XMS.Application.Abstractions
{
    public interface ICrudService<TEntity>
    {
        Task CreateAsync(TEntity item, CancellationToken ct = default);
        Task UpdateAsync(TEntity item, CancellationToken ct = default);
        Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetListAsync(CancellationToken ct = default);
    }
}
