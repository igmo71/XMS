using XMS.Integration.OneC.Api;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Modules.CostModule.Abstractions;

namespace XMS.Modules.CostModule.Application;

internal class CostAllocationService(IOneCUtService utService) : ICostAllocationService
{
    public async Task<IReadOnlyList<Document_РасходныйКассовыйОрдер>> GetDocument_РасходныйКассовыйОрдер_Async(
        DocumentQueryParameters parameters, CancellationToken ct = default)
    {
        return await utService.GetDocument_РасходныйКассовыйОрдер_Async(parameters, ct);
    }

    public async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(
        DocumentQueryParameters parameters, CancellationToken ct = default)
    {
        return await utService.GetDocument_СписаниеБезналичныхДенежныхСредств_Async(parameters, ct);
    }
}
