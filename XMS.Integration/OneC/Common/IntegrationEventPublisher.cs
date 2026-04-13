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
        //var operationName = $"{nameof(IntegrationEventPublisher)}.{nameof(Publish)}";
        //using var activity = AppTelemetry.ActivitySource.StartActivity(operationName, ActivityKind.Consumer)
        //    ?? new Activity(operationName).Start();

        await publisher.PublishAsync(IntegrationHelper.GetExchangeName<TEntity>(hostEnvironment), catalogEvent);

        return TypedResults.Ok();
    }
}
