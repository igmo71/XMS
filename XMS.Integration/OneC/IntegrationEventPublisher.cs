using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Common;

namespace XMS.Integration.OneC;

internal static class IntegrationEventPublisher
{
    public static async Task<IResult> Publish<TEntity>(HttpContext httpContext,
        [FromServices] IEventPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] CatalogEvent catalogEvent) where TEntity : ISyncable
    {
        await publisher.PublishAsync(SyncHelper.GetExchangeName<TEntity>(hostEnvironment), catalogEvent);

        return TypedResults.Ok();
    }
}
