using XMS.Application.Common;
using XMS.Application.Integration.OneC.Common;

namespace XMS.Application.Abstractions.Integration.OneC;

public interface ICatalogService<TEntity> where TEntity : Catalog
{
    Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default);
    Task<ServiceResult> ResyncAsync(CancellationToken ct = default);
}
