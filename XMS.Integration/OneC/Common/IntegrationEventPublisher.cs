using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.EventBus;

namespace XMS.Integration.OneC.Common;

internal static class IntegrationEventPublisher
{
    public static async Task<IResult> PublishCatalogNotificationAsync<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IEventNamingService eventNaming,
        [FromServices] ILogger<CatalogNotification> logger,
        [FromBody] CatalogNotification catalogNotification)
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} {Request.Path} {@catalogNotification}",
                nameof(PublishCatalogNotificationAsync), httpContext.Request.Path.Value, catalogNotification);

        await publisher.PublishAsync(eventNaming.GetEventName<TEntity>(), catalogNotification);

        return TypedResults.Ok();
    }


    public static async Task<IResult> PublishDocumentNotificationAsync<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IEventNamingService eventNaming,
        [FromServices] ILogger<DocumentNotification> logger,
        [FromBody] DocumentNotification documentNotification)
    {


        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} {Request.Path} {@documentNotification}",
                nameof(PublishDocumentNotificationAsync), httpContext.Request.Path.Value, documentNotification);

        await publisher.PublishAsync(eventNaming.GetEventName<TEntity>(), documentNotification);

        return TypedResults.Ok();
    }
}
