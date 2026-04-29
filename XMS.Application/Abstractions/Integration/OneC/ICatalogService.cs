using XMS.Application.Common;
using XMS.Integrations.OneC.Common;

namespace XMS.Integrations.OneC;

public interface ICatalogService<TEntity> where TEntity : Catalog
{
    Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default);
    Task<ServiceResult> ResyncAsync(CancellationToken ct = default);
}
