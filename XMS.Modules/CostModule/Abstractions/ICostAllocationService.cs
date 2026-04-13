using XMS.Integration.OneC.Common;

namespace XMS.Modules.CostModule.Abstractions;

public interface ICostAllocationService
{
    Task<IReadOnlyList<XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер>> GetDocument_РасходныйКассовыйОрдер_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<IReadOnlyList<XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default);
}
