using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Integration.OneC.Events;

namespace XMS.Application.Integration.OneC.Common;

internal static class CatalogPublisher
{
    public static async Task<IResult> PublishAsync<TEntity>(HttpContext httpContext,
        [FromServices] IIntegrationEventPublisher publisher,
        [FromServices] ILogger<TEntity> logger,
        [FromBody] ICatalog catalogNotification)
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
    public static async Task<IResult> PublishAsync<TEvent>(HttpContext httpContext,
        [FromServices] IIntegrationEventPublisher publisher,
        [FromServices] ILogger<TEvent> logger,
        [FromBody] TEvent documentNotification) where TEvent : class, IOdataEntity
    {
        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} {Request.Path} {@documentNotification}",
                nameof(PublishAsync), httpContext.Request.Path.Value, documentNotification);

        await publisher.PublishAsync(documentNotification);

        return TypedResults.Ok();
    }
}
