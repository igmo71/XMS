using XMS.Integrations.OneC.Common;
using XMS.Integrations.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Integrations.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Modules.CostModule.Application;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostAllocationService
{
    Task<CostAllocationDto> GetListAsync(CostAllocationQueryParameters queryParameters, CancellationToken token);
    Task UpdateAsync(CostAllocation item, CancellationToken ct = default);

    Task<IReadOnlyList<Document_РасходныйКассовыйОрдер>> GetDocumentРасходныйКассовыйОрдерAsync(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default);

}
