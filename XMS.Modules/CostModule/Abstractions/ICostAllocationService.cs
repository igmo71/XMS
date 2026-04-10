namespace XMS.Modules.CostModule.Abstractions;

public interface ICostAllocationService
{
    Task<IReadOnlyList<XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер>> GetDocument_РасходныйКассовыйОрдер_Async(XMS.Integration.OneC.DocumentQueryParameters parameters, CancellationToken ct = default);
    Task<IReadOnlyList<XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(XMS.Integration.OneC.DocumentQueryParameters parameters, CancellationToken ct = default);
}
