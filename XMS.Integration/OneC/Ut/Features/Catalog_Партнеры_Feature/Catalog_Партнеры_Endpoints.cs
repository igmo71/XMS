using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature
{
    public static class Catalog_Партнеры_Endpoints
    {
        public static IEndpointRouteBuilder MapDocument_Catalog_Партнеры_Endpoints(this IEndpointRouteBuilder builder)
        {
            var catalogGroup = builder.MapGroup("/integration/1c/ut")
                .WithTags("Integration 1C UT");

            catalogGroup.MapPatch("/notify/catalog-партнеры",
                PublishDocument_Catalog_Партнеры)
                    .WithName(nameof(PublishDocument_Catalog_Партнеры))
                    .WithSummary(nameof(PublishDocument_Catalog_Партнеры))
                    .WithDescription("Notify Document_СписаниеБезналичныхДенежныхСредств");

            catalogGroup.MapPut("/resync/catalog-партнеры",
               ResyncDocument_Catalog_Партнеры)
                   .WithName(nameof(ResyncDocument_Catalog_Партнеры))
                   .WithSummary(nameof(ResyncDocument_Catalog_Партнеры))
                   .WithDescription("Resync Document_СписаниеБезналичныхДенежныхСредств from OneS Ut for a cpecific date period and save them to the DB");

            return builder;
        }

        private static async Task<IResult> PublishDocument_Catalog_Партнеры(HttpContext httpContext,
            [FromServices] IPublishEndpoint publishEndpoint,
            [FromServices] ILogger<Catalog_Партнеры_Changed> logger,
            [FromBody] Catalog_Партнеры_Changed oneCNotifyMessage)
        {
            logger.LogDebug("{Request.Method} {Request.Path} {@OneCNotifyMessage}",
                httpContext.Request.Method, httpContext.Request.Path, oneCNotifyMessage);

            await publishEndpoint.Publish(oneCNotifyMessage);

            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<string>>>
            ResyncDocument_Catalog_Партнеры(
                [FromServices] ICatalog_Партнеры_Service catalogService)
        {
            await catalogService.ResyncByDateRangeAsync();

            return TypedResults.Ok();
        }
    }
}
