using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature
{
    public static class Document_СписаниеБезналичныхДенежныхСредств_Endpoints
    {
        public static IEndpointRouteBuilder MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints(this IEndpointRouteBuilder builder)
        {
            var utGroup = builder.MapGroup("/integration/1c/ut")
                .WithTags("Integration 1C UT");

            utGroup.MapGet("/document-списание-безналичных-денежных-средств",
                GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate)
                    .WithName(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
                    .WithSummary(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
                    .WithDescription("Get Document_СписаниеБезналичныхДенежныхСредств by Ref_Key");

            utGroup.MapGet("/document-списание-безналичных-денежных-средств/{refKey}",
                GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey)
                    .WithName(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey))
                    .WithSummary(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey))
                    .WithDescription("Get Document_СписаниеБезналичныхДенежныхСредств by Ref_Key");

            utGroup.MapPut("/resync/document-списание-безналичных-денежных-средств",
                ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate)
                    .WithName(nameof(ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
                    .WithSummary(nameof(ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
                    .WithDescription("Resync Document_СписаниеБезналичныхДенежныхСредств from OneS Ut for a cpecific date period and save them to the DB");

            utGroup.MapPatch("/notify/document-списание-безналичных-денежных-средств",
                PublishDocument_СписаниеБезналичныхДенежныхСредств)
                    .WithName(nameof(PublishDocument_СписаниеБезналичныхДенежныхСредств))
                    .WithSummary(nameof(PublishDocument_СписаниеБезналичныхДенежныхСредств))
                    .WithDescription("Notify Document_СписаниеБезналичныхДенежныхСредств");

            return builder;
        }

        private static async Task<Results<Ok<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>>, BadRequest<string>>>
            GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate(
                [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
                [FromQuery] string? numberTerm = null,
                [FromQuery] DateTime? from = null,
                [FromQuery] DateTime? to = null,
                [FromQuery] int? skip = AppSettings.Default.Skip,
                [FromQuery] int? take = AppSettings.Default.Take,
                CancellationToken ct = default)
        {
            var parameters = new DocumentQueryParameters(numberTerm, from, to, skip, take);

            var result = await documentService.GetListAsync(parameters, ct);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok<Document_СписаниеБезналичныхДенежныхСредств>, NotFound<string>>>
            GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey(
                [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
                [FromRoute] Guid refKey,
                CancellationToken ct = default)
        {
            var result = await documentService.GetAsync(refKey, ct);

            return result is null ? TypedResults.NotFound($"Document Not Found by Ref_Key {refKey}") : TypedResults.Ok(result);
        }

        private static async Task<Results<Ok, BadRequest<string>>>
            ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate(
                [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to)
        {
            await documentService.ResyncByDateRangeAsync(from, to);

            return TypedResults.Ok();
        }

        private static async Task<IResult> PublishDocument_СписаниеБезналичныхДенежныхСредств(HttpContext httpContext,
            [FromServices] IRabbitPublisher publisher,
            [FromServices] ILogger<Document_СписаниеБезналичныхДенежныхСредств_Changed> logger,
            [FromBody] Document_СписаниеБезналичныхДенежныхСредств_Changed oneCNotifyMessage)
        {
            logger.LogDebug("{Request.Method} {Request.Path} {@OneCNotifyMessage}",
                httpContext.Request.Method, httpContext.Request.Path, oneCNotifyMessage);

            await publisher.PublishAsync(nameof(Document_СписаниеБезналичныхДенежныхСредств), oneCNotifyMessage);

            return TypedResults.Ok();
        }
    }
}
