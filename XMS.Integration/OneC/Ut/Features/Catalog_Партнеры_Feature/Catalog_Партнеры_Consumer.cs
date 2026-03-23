using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature
{
    internal class Catalog_Партнеры_Consumer(
        ICatalog_Партнеры_Service catalogService,
        ILogger<Catalog_Партнеры_Consumer> logger)
        : IOneCConsumer<Catalog_Партнеры_Changed>
    {
        public async Task Consume(ConsumeContext<Catalog_Партнеры_Changed> context)
        {
            logger.LogDebug("{Source} - Start {@Message}", nameof(Consume), context.Message);
            await catalogService.HandleEventOneC(context.Message); // TODO Handle Error, send to error queue
            logger.LogDebug("{Source} - Ok {@Message}", nameof(Consume), context.Message);
        }
    }
}
