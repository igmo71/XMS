using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Modules.CostModule.Abstractions.Integration;

internal interface IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler
{
    Task HandleReceivedAsync(Document_СписаниеБезналичныхДенежныхСредств_Dto dto, CancellationToken ct = default);
    Task HandleDeletedAsync(Document_СписаниеБезналичныхДенежныхСредств_Dto dto, CancellationToken ct = default);
}
