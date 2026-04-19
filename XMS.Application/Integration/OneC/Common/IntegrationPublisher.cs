using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace XMS.Application.Integration.OneC.Common;

internal static class CatalogPublisher
{
    public static async Task<IResult> PublishAsync<TEntity>(HttpContext httpContext,
        [FromServices] IIntegrationEventPublisher publisher,
        [FromServices] ILogger<CatalogNotification> logger,
        [FromBody] CatalogNotification catalogNotification)
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} {Request.Path} {@catalogNotification}",
                nameof(PublishAsync), httpContext.Request.Path.Value, catalogNotification);

        await publisher.PublishAsync(catalogNotification);

        return TypedResults.Ok();
    }
}

internal static class DocumentPublisher
{
    public static async Task<IResult> PublishAsync<TEntity>(HttpContext httpContext,
        [FromServices] IIntegrationEventPublisher publisher,
        [FromServices] ILogger<DocumentNotification> logger,
        [FromBody] DocumentNotification documentNotification)
    {


        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} {Request.Path} {@documentNotification}",
                nameof(PublishAsync), httpContext.Request.Path.Value, documentNotification);

        await publisher.PublishAsync(documentNotification);

        return TypedResults.Ok();
    }
}
