using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

public static class Document_СписаниеБезналичныхДенежныхСредств_Endpoints
{
    public static IEndpointRouteBuilder MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/document-списание-безналичных-денежных-средств")
            .WithTags("1C UT Document_СписаниеБезналичныхДенежныхСредств");

        apiGroup.MapGet("/", GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate)
            .WithName(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
            .WithSummary(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
            .WithDescription("Get Document_СписаниеБезналичныхДенежныхСредств by Date from DB");

        apiGroup.MapGet("/{refKey}", GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey)
            .WithName(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey))
            .WithSummary(nameof(GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey))
            .WithDescription("Get Document_СписаниеБезналичныхДенежныхСредств By Date from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/document-списание-безналичных-денежных-средств")
            .WithTags("1C UT Document_СписаниеБезналичныхДенежныхСредств");

        extGroup.MapPatch("/notify", NotifyDocument_СписаниеБезналичныхДенежныхСредств)
            .WithName(nameof(NotifyDocument_СписаниеБезналичныхДенежныхСредств))
            .WithSummary(nameof(NotifyDocument_СписаниеБезналичныхДенежныхСредств))
            .WithDescription("Notify Document_СписаниеБезналичныхДенежныхСредств");

        extGroup.MapPut("/resync", ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate)
            .WithName(nameof(ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
            .WithSummary(nameof(ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate))
            .WithDescription("Resync Document_СписаниеБезналичныхДенежныхСредств from OneS Ut and save to DB");

        return builder;
    }

    private static async Task<Results<Ok<Document_СписаниеБезналичныхДенежныхСредств>, NotFound>> GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey(
        [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await documentService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>>, BadRequest>> GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate(
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

    private static async Task<IResult> NotifyDocument_СписаниеБезналичныхДенежныхСредств(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] DocumentEvent documentEvent)
    {
        await publisher.PublishAsync(Document_СписаниеБезналичныхДенежныхСредств.GetExchangeName(hostEnvironment), documentEvent);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest>> ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate(
        [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        await documentService.ResyncAsync(from, to);

        return TypedResults.Ok();
    }
}
