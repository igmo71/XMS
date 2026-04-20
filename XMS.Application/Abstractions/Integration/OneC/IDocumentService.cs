using XMS.Application.Common;
using XMS.Application.Integration.OneC.Common;

namespace XMS.Application.Abstractions.Integration.OneC;

public interface IDocumentService<TEntity> where TEntity : Document
{
    Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<ServiceResult> ResyncAsync(DateTime from, DateTime to, CancellationToken ct = default);
}
