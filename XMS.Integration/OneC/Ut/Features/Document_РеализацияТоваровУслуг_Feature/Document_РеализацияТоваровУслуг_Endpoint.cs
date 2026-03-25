using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

public static class Document_РеализацияТоваровУслуг_Endpoint
{
    public static IEndpointRouteBuilder MapDocument_РеализацияТоваровУслуг_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/document-реализация-товаров-услуг")
            .WithTags("1C UT Document_РеализацияТоваровУслуг");

        apiGroup.MapGet("/", GetDocument_РеализацияТоваровУслуг_ByDate)
            .WithName(nameof(GetDocument_РеализацияТоваровУслуг_ByDate))
            .WithSummary(nameof(GetDocument_РеализацияТоваровУслуг_ByDate))
            .WithDescription("Get Document_РеализацияТоваровУслуг by Date from DB");

        apiGroup.MapGet("/{refKey}", GetDocument_РеализацияТоваровУслуг_ByRefKey)
            .WithName(nameof(GetDocument_РеализацияТоваровУслуг_ByRefKey))
            .WithSummary(nameof(GetDocument_РеализацияТоваровУслуг_ByRefKey))
            .WithDescription("Get Document_РеализацияТоваровУслуг By Date from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/document-реализация-товаров-услуг")
            .WithTags("1C UT Document_РеализацияТоваровУслуг");

        extGroup.MapPatch("/notify", NotifyDocument_РеализацияТоваровУслуг)
            .WithName(nameof(NotifyDocument_РеализацияТоваровУслуг))
            .WithSummary(nameof(NotifyDocument_РеализацияТоваровУслуг))
            .WithDescription("Notify Document_РеализацияТоваровУслуг");

        extGroup.MapPut("/resync", ResyncDocument_РеализацияТоваровУслуг_ByDate)
            .WithName(nameof(ResyncDocument_РеализацияТоваровУслуг_ByDate))
            .WithSummary(nameof(ResyncDocument_РеализацияТоваровУслуг_ByDate))
            .WithDescription("Resync Document_РеализацияТоваровУслуг from OneS Ut and save to DB");

        return builder;
    }

    private static async Task<Results<Ok<Document_РеализацияТоваровУслуг>, NotFound>> GetDocument_РеализацияТоваровУслуг_ByRefKey(
        [FromServices] IDocument_РеализацияТоваровУслуг_Service documentService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await documentService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Document_РеализацияТоваровУслуг>>, BadRequest>> GetDocument_РеализацияТоваровУслуг_ByDate(
        [FromServices] IDocument_РеализацияТоваровУслуг_Service documentService,
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

    private static async Task<IResult> NotifyDocument_РеализацияТоваровУслуг(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] DocumentEvent documentEvent)
    {
        await publisher.PublishAsync(Document_РеализацияТоваровУслуг.GetExchangeName(hostEnvironment), documentEvent);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, BadRequest>> ResyncDocument_РеализацияТоваровУслуг_ByDate(
        [FromServices] IDocument_РеализацияТоваровУслуг_Service documentService,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        await documentService.ResyncAsync(from, to);

        return TypedResults.Ok();
    }
}
