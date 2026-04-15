using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Common;

internal static class IntegrationEventPublisher
{
    public static async Task<IResult> PublishCatalogNotificationAsync<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromServices] ILogger<CatalogNotification> logger,
        [FromBody] CatalogNotification catalogNotification) where TEntity : ISyncable
    {
        await publisher.PublishAsync(IntegrationHelper.GetEventName<TEntity>(IntegrationType.Notify, hostEnvironment), catalogNotification);

        logger.LogDebug("{Source} {Request.Path} {@catalogNotification}", nameof(PublishCatalogNotificationAsync), httpContext.Request.Path.Value, catalogNotification);

        return TypedResults.Ok();
    }


    public static async Task<IResult> PublishDocumentNotificationAsync<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromServices] ILogger<DocumentNotification> logger,
        [FromBody] DocumentNotification documentNotification) where TEntity : ISyncable
    {
        await publisher.PublishAsync(IntegrationHelper.GetEventName<TEntity>(IntegrationType.Notify, hostEnvironment), documentNotification);

        logger.LogDebug("{Source} {Request.Path} {@documentNotification}", nameof(PublishDocumentNotificationAsync), httpContext.Request.Path.Value, documentNotification);

        return TypedResults.Ok();
    }

    public static async Task<IResult> PublishDocumentNotificationPostAsync<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromServices] ILogger<DocumentNotification> logger,
        [FromBody] DocumentNotification documentNotification) where TEntity : ISyncable
    {
        await publisher.PublishAsync(IntegrationHelper.GetEventName<TEntity>(IntegrationType.Notify, hostEnvironment), documentNotification);

        logger.LogDebug("{Source} {Request.Path} {@documentNotification}", nameof(PublishDocumentNotificationPostAsync), httpContext.Request.Path.Value, documentNotification);

        return TypedResults.Ok();
    }
}
