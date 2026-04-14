using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

namespace XMS.Modules.CostModule.Abstractions.Integration;

internal interface IDocument_РасходныйКассовыйОрдер_EventHandler
{
    Task HandleReceivedAsync(Document_РасходныйКассовыйОрдер_Dto dto, CancellationToken ct = default);
    Task HandleDeletedAsync(Document_РасходныйКассовыйОрдер_Dto dto, CancellationToken ct = default);
}
