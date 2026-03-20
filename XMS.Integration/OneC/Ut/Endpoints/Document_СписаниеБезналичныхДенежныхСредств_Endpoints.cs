using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using XMS.Core;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Models;
using XMS.Modules.CostModule.EventBus;

namespace XMS.Integration.OneC.Ut.Endpoints
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

            utGroup.MapPut("/document-списание-безналичных-денежных-средств/resync",
                ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate)
                    .WithName(nameof(ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
                    .WithSummary(nameof(ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
                    .WithDescription("Resync Document_СписаниеБезналичныхДенежныхСредств from OneS Ut for a cpecific date period and save them to the DB");

            utGroup.MapPost("/document-списание-безналичных-денежных-средств/notify",
                NotifyDocument_СписаниеБезналичныхДенежныхСредств_Changed)
                    .WithName(nameof(NotifyDocument_СписаниеБезналичныхДенежныхСредств_Changed))
                    .WithSummary(nameof(NotifyDocument_СписаниеБезналичныхДенежныхСредств_Changed))
                    .WithDescription("Notify Document_СписаниеБезналичныхДенежныхСредств Changed");

            utGroup.MapDelete("/document-списание-безналичных-денежных-средств/notify",
                NotifyDocument_СписаниеБезналичныхДенежныхСредств_Deleted)
                    .WithName(nameof(NotifyDocument_СписаниеБезналичныхДенежныхСредств_Deleted))
                    .WithSummary(nameof(NotifyDocument_СписаниеБезналичныхДенежныхСредств_Deleted))
                    .WithDescription("Notify Document_СписаниеБезналичныхДенежныхСредств Changed");

            return builder;
        }

        private static async Task<Results<Ok<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>>, BadRequest<string>>>
            GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate(
                [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
                [FromServices] ILoggerFactory loggerFactory,
                [FromQuery] string? numberTerm = null,
                [FromQuery] DateTime? from = null,
                [FromQuery] DateTime? to = null,
                [FromQuery] int? skip = AppSettings.Default.Skip,
                [FromQuery] int? take = AppSettings.Default.Take,
                CancellationToken ct = default)
        {
            var logger = loggerFactory.CreateLogger(nameof(Document_СписаниеБезналичныхДенежныхСредств_Endpoints));

            var parameters = new DocumentQueryParameters(numberTerm, from, to, skip, take);

            var result = await documentService.GetListAsync(parameters, ct);

            logger.LogDebug("{Source} {DocumentQueryParameters} {Documents}",
                nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate), parameters, result);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok<Document_СписаниеБезналичныхДенежныхСредств>, NotFound<string>>>
            GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey(
                [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
                [FromServices] ILoggerFactory loggerFactory,
                [FromRoute] Guid refKey,
                CancellationToken ct = default)
        {
            var logger = loggerFactory.CreateLogger(nameof(Document_СписаниеБезналичныхДенежныхСредств_Endpoints));

            var result = await documentService.GetAsync(refKey, ct);

            logger.LogDebug("{Source} {DocumentQueryParameters} {Document}",
                nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey), refKey, result);

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

        private static async Task<IResult> NotifyDocument_СписаниеБезналичныхДенежныхСредств_Changed(
            [FromServices] IPublishEndpoint publishEndpoint,
            [FromBody] Document_СписаниеБезналичныхДенежныхСредств_Changed notifyBody)
        {
            notifyBody.EventOperation = EventOperation.Changed;

            await publishEndpoint.Publish(notifyBody);

            return TypedResults.Ok();
        }

        private static async Task<IResult> NotifyDocument_СписаниеБезналичныхДенежныхСредств_Deleted(
            [FromServices] IPublishEndpoint publishEndpoint,
            [FromBody] Document_СписаниеБезналичныхДенежныхСредств_Changed notifyBody)
        {
            notifyBody.EventOperation = EventOperation.Delete;

            await publishEndpoint.Publish(notifyBody);

            return TypedResults.Ok();
        }
    }
}
