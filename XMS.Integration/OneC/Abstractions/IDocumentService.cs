using XMS.Core.Common;

namespace XMS.Integration.OneC.Abstractions;

public interface IDocumentService<TEntity> where TEntity : class, IDocument
{
    Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<ServiceResult> ResyncAsync(DateTime from, DateTime to, CancellationToken ct = default);
}
