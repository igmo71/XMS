using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Common;

internal static class IntegrationEventPublisher
{
    public static async Task<IResult> Publish<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] CatalogEvent catalogEvent) where TEntity : ISyncable
    {
        await publisher.PublishAsync(IntegrationHelper.GetEventName<TEntity>(IntegrationType.Notify, hostEnvironment), catalogEvent);

        return TypedResults.Ok();
    }
}
