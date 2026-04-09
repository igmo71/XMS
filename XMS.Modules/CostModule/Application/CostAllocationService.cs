using XMS.Modules.CostModule.Abstractions;

namespace XMS.Modules.CostModule.Application;

internal class CostAllocationService(XMS.Integration.OneC.Api.IOneCUtService utService) : ICostAllocationService
{
    public async Task<IReadOnlyList<XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature.Document_РасходныйКассовыйОрдер>> GetDocument_РасходныйКассовыйОрдер_Async(XMS.Integration.OneC.DocumentQueryParameters parameters, CancellationToken ct = default)
    {
        return await utService.GetDocument_РасходныйКассовыйОрдер_Async(parameters, ct);
    }

    public async Task<IReadOnlyList<XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature.Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(XMS.Integration.OneC.DocumentQueryParameters parameters, CancellationToken ct = default)
    {
        return await utService.GetDocument_СписаниеБезналичныхДенежныхСредств_Async(parameters, ct);
    }
}
