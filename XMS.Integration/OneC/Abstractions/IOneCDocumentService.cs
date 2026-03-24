using XMS.Core.Common;

namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCDocumentService<TEntity, TEvent>
        where TEntity : class, IOneCDocument
        where TEvent : DocumentNotifyMessage
    {
        Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default);
        Task<ServiceResult> ResyncAsync(DateTime from, DateTime to, CancellationToken ct = default);
        Task<ServiceResult> HandleEventOneC(TEvent oneCNotifyMessage, CancellationToken ct = default);
    }
}
