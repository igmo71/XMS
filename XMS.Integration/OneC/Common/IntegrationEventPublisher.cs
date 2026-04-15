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
        [FromBody] CatalogNotification catalogEvent) where TEntity : ISyncable
    {
        await publisher.PublishAsync(IntegrationHelper.GetEventName<TEntity>(IntegrationType.Notify, hostEnvironment), catalogEvent);

        logger.LogDebug("{Source} {Request.Path} {@catalogEvent}", nameof(PublishCatalogNotificationAsync), httpContext.Request.Path.Value, catalogEvent);

        return TypedResults.Ok();
    }


    public static async Task<IResult> PublishDocumentNotificationAsync<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromServices] ILogger<DocumentNotification> logger,
        [FromBody] DocumentNotification documentEvent) where TEntity : ISyncable
    {
        await publisher.PublishAsync(IntegrationHelper.GetEventName<TEntity>(IntegrationType.Notify, hostEnvironment), documentEvent);

        logger.LogDebug("{Source} {Request.Path} {@documentEvent}", nameof(PublishDocumentNotificationAsync), httpContext.Request.Path.Value, documentEvent);

        return TypedResults.Ok();
    }

    public static async Task<IResult> PublishDocumentNotificationPostAsync<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromServices] ILogger<DocumentNotification> logger,
        [FromBody] DocumentNotification documentEvent) where TEntity : ISyncable
    {
        await publisher.PublishAsync(IntegrationHelper.GetEventName<TEntity>(IntegrationType.Notify, hostEnvironment), documentEvent);

        logger.LogDebug("{Source} {Request.Path} {@documentEvent}", nameof(PublishDocumentNotificationAsync), httpContext.Request.Path.Value, documentEvent);

        return TypedResults.Ok();
    }
}
