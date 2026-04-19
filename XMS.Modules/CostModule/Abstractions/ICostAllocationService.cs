using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostAllocationService
{
    Task<IReadOnlyList<CostAllocation>> GetListAsync(bool includeDeleted = false, CancellationToken ct = default);
    Task UpdateAsync(CostAllocation item, CancellationToken ct = default);

    Task<IReadOnlyList<Document_РасходныйКассовыйОрдер>> GetDocumentРасходныйКассовыйОрдерAsync(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
}
