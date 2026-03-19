using XMS.Application.Common;
using XMS.Core.Common;

namespace XMS.Application.Abstractions
{
    public interface ICrudService<TEntity>
    {
        Task CreateAsync(TEntity item, CancellationToken ct = default);
        Task UpdateAsync(TEntity item, CancellationToken ct = default);
        Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<ServiceResult> RestoreAsync(Guid id, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

        // TODO: Свести у одному методу
        Task<IReadOnlyList<TEntity>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetListAsync(QueryParameters searchParameters, CancellationToken ct = default);
    }
}
