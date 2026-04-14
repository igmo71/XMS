using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostAllocationService
{
    Task HandleDocument_СписаниеБезналичныхДенежныхСредств_ReceivedAsync(Document_РасходныйКассовыйОрдер_Dto dto, CancellationToken ct = default);
    Task HandleDocument_СписаниеБезналичныхДенежныхСредств_DeletedAsync(Document_РасходныйКассовыйОрдер_Dto dto, CancellationToken ct = default);

    Task<IReadOnlyList<Document_РасходныйКассовыйОрдер>> GetDocumentРасходныйКассовыйОрдерAsync(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
}
