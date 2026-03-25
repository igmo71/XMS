using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut.Abstractions;

internal interface IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler
    : IOneCEventHandler<Document_СписаниеБезналичныхДенежныхСредств_Changed>
{
}
