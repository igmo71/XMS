using MassTransit;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Modules.CostModule.EventBus;

namespace XMS.Integration.OneC.Ut.EventBus
{
    public class Document_СписаниеБезналичныхДенежныхСредств_Consumer(
        IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService) : IOneCConsumer<Document_СписаниеБезналичныхДенежныхСредств_Changed>
    {
        public async Task Consume(ConsumeContext<Document_СписаниеБезналичныхДенежныхСредств_Changed> context)
        {
            _ = context.Message.EventOperation switch
            {
                EventOperation.Changed => await documentService.CreateOrUpdateAsync(context.Message.Ref_Key),
                EventOperation.Delete => await documentService.DeleteAsync(context.Message.Ref_Key),
                _ => throw new InvalidOperationException($"Unknown operation: {context.Message.EventOperation}")
            };
        }
    }
}
