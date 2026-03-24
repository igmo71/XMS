using XMS.Core.Common;

namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCCatalogService<TEntity, TEvent>
        where TEntity : class, IOneCCatalog
        where TEvent : CatalogNotifyMessage
    {
        Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetListAsync(CatalogQueryParameters parameters, CancellationToken ct = default);
        Task<ServiceResult> ResyncAsync(CancellationToken ct = default);
        Task<ServiceResult> HandleEventOneC(TEvent oneCNotifyMessage, CancellationToken ct = default);
    }
}
