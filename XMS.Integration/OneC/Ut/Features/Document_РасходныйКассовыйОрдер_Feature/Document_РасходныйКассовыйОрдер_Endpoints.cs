using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

public static class Document_РасходныйКассовыйОрдер_Endpoints
{
    public static IEndpointRouteBuilder MapDocument_РасходныйКассовыйОрдер_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/document-расходный-кассовый-ордер")
            .WithTags("1C UT Document_РасходныйКассовыйОрдер");

        apiGroup.MapGet("/", GetDocument_РасходныйКассовыйОрдер_ByDate)
            .WithName(nameof(GetDocument_РасходныйКассовыйОрдер_ByDate))
            .WithSummary(nameof(GetDocument_РасходныйКассовыйОрдер_ByDate))
            .WithDescription("Get Document_РасходныйКассовыйОрдер by Date from DB");

        apiGroup.MapGet("/{refKey}", GetDocument_РасходныйКассовыйОрдер_ByRefKey)
            .WithName(nameof(GetDocument_РасходныйКассовыйОрдер_ByRefKey))
            .WithSummary(nameof(GetDocument_РасходныйКассовыйОрдер_ByRefKey))
            .WithDescription("Get Document_РасходныйКассовыйОрдер By Date from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/document-расходный-кассовый-ордер")
            .WithTags("1C UT Document_РасходныйКассовыйОрдер");

        extGroup.MapPatch("/notify", NotifyDocument_РасходныйКассовыйОрдер)
            .WithName(nameof(NotifyDocument_РасходныйКассовыйОрдер))
            .WithSummary(nameof(NotifyDocument_РасходныйКассовыйОрдер))
            .WithDescription("Notify Document_РасходныйКассовыйОрдер");

        extGroup.MapPut("/resync", ResyncDocument_РасходныйКассовыйОрдер_ByDate)
            .WithName(nameof(ResyncDocument_РасходныйКассовыйОрдер_ByDate))
            .WithSummary(nameof(ResyncDocument_РасходныйКассовыйОрдер_ByDate))
            .WithDescription("Resync Document_РасходныйКассовыйОрдер from OneS Ut and save to DB");

        return builder;
    }

    private static async Task<Results<Ok<Document_РасходныйКассовыйОрдер>, NotFound>> GetDocument_РасходныйКассовыйОрдер_ByRefKey(
        [FromServices] IDocument_РасходныйКассовыйОрдер_Service documentService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await documentService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Document_РасходныйКассовыйОрдер>>, BadRequest>> GetDocument_РасходныйКассовыйОрдер_ByDate(
        [FromServices] IDocument_РасходныйКассовыйОрдер_Service documentService,
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

    private static async Task<IResult> NotifyDocument_РасходныйКассовыйОрдер(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] DocumentEvent documentEvent)
    {
        await publisher.PublishAsync(Document_РасходныйКассовыйОрдер.GetExchangeName(hostEnvironment), documentEvent);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest>> ResyncDocument_РасходныйКассовыйОрдер_ByDate(
        [FromServices] IDocument_РасходныйКассовыйОрдер_Service documentService,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        await documentService.ResyncAsync(from, to);

        return TypedResults.Ok();
    }
}
