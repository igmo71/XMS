using XMS.Core.Common;

namespace XMS.Integration.OneC.Abstractions;

public interface ICatalogService<TEntity> where TEntity : class, ICatalog
{
    Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default);
    Task<ServiceResult> ResyncAsync(CancellationToken ct = default);
}
