using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

public static class Document_ЗаказКлиента_Endpoints
{
    public static IEndpointRouteBuilder MapDocument_ЗаказКлиента_Endpoints(this IEndpointRouteBuilder builder)
    {

        var apiGroup = builder.MapGroup("/api/1c/ut/document-заказ-клиента")
            .WithTags("1C UT Document_ЗаказКлиента");

        apiGroup.MapGet("/", GetDocument_ЗаказКлиента_ByDate)
            .WithName(nameof(GetDocument_ЗаказКлиента_ByDate))
            .WithSummary(nameof(GetDocument_ЗаказКлиента_ByDate))
            .WithDescription("Get Document_ЗаказКлиента by Date from DB");

        apiGroup.MapGet("/{refKey}", GetDocument_ЗаказКлиента_ByRefKey)
            .WithName(nameof(GetDocument_ЗаказКлиента_ByRefKey))
            .WithSummary(nameof(GetDocument_ЗаказКлиента_ByRefKey))
            .WithDescription("Get Document_ЗаказКлиента By Date from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/document-заказ-клиента")
            .WithTags("1C UT Document_ЗаказКлиента");

        extGroup.MapPatch("/notify", NotifyDocument_ЗаказКлиента)
            .WithName(nameof(NotifyDocument_ЗаказКлиента))
            .WithSummary(nameof(NotifyDocument_ЗаказКлиента))
            .WithDescription("Notify Document_ЗаказКлиента");

        extGroup.MapPut("/resync", ResyncDocument_ЗаказКлиента_ByDate)
            .WithName(nameof(ResyncDocument_ЗаказКлиента_ByDate))
            .WithSummary(nameof(ResyncDocument_ЗаказКлиента_ByDate))
            .WithDescription("Resync Document_ЗаказКлиента from OneS Ut and save to DB");

        return builder;
    }

    private static async Task<Results<Ok<Document_ЗаказКлиента>, NotFound>> GetDocument_ЗаказКлиента_ByRefKey(
        [FromServices] IDocument_ЗаказКлиента_Service documentService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await documentService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Document_ЗаказКлиента>>, BadRequest>> GetDocument_ЗаказКлиента_ByDate(
        [FromServices] IDocument_ЗаказКлиента_Service documentService,
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

    private static async Task<IResult> NotifyDocument_ЗаказКлиента(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] DocumentEvent documentEvent)
    {
        await publisher.PublishAsync(Document_ЗаказКлиента.GetExchangeName(hostEnvironment), documentEvent);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest>> ResyncDocument_ЗаказКлиента_ByDate(
        [FromServices] IDocument_ЗаказКлиента_Service documentService,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        await documentService.ResyncAsync(from, to);

        return TypedResults.Ok();
    }
}
