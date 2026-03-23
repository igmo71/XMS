using MassTransit;
using Microsoft.Extensions.Logging;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature
{
    public class Document_СписаниеБезналичныхДенежныхСредств_Consumer(
        IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
        ILogger<Document_СписаниеБезналичныхДенежныхСредств_Consumer> logger)
        : IOneCConsumer<Document_СписаниеБезналичныхДенежныхСредств_Changed>
    {
        public async Task Consume(ConsumeContext<Document_СписаниеБезналичныхДенежныхСредств_Changed> context)
        {
            logger.LogDebug("{Source} - Start {@Message}", nameof(Consume), context.Message);
            await documentService.HandleEventOneC(context.Message); // TODO Handle Error, send to error queue
            logger.LogDebug("{Source} - Ok {@Message}", nameof(Consume), context.Message);
        }
    }
}
