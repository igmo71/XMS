using MassTransit;
using XMS.Application.Common;
using XMS.Integration.OneC;
using XMS.Modules.CostModule.Abstractions;

namespace XMS.Modules.CostModule.EventBus
{
    public class Document_СписаниеБезналичныхДенежныхСредств_Consumer(
        IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService) : IConsumer<Document_СписаниеБезналичныхДенежныхСредств_Changed>
    {
        public async Task Consume(ConsumeContext<Document_СписаниеБезналичныхДенежныхСредств_Changed> context)
        {
            _ = context.Message.EventOperation switch
            {
                EventOperation.CreateOrUpdate => await documentService.CreateOrUpdateAsync(context.Message.Ref_Key),
                EventOperation.Delete => await documentService.DeleteAsync(context.Message.Ref_Key),
                _ => throw new InvalidOperationException($"Unknown operation: {context.Message.EventOperation}")
            };
        }
    }
}
