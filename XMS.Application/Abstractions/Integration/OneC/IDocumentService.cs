using XMS.Application.Common;
using XMS.Integrations.OneC.Common;

namespace XMS.Integrations.OneC;

public interface IDocumentService<TEntity> where TEntity : Document
{
    Task<TEntity?> GetAsync(Guid refKey, CancellationToken ct = default);
    Task<TEntity?> GetByBarcodeAsync(string barcode, CancellationToken ct);
    Task<IReadOnlyList<TEntity>> GetListAsync(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<ServiceResult> ResyncAsync(DateTime from, DateTime to, CancellationToken ct = default);
}
