using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Application.Abstractions.Integration.OneC.Ut;
using XMS.Application.Common;
using XMS.Application.Integration.OneC.Common;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;

public static class Document_РасходныйКассовыйОрдер_Endpoints
{
    public static IEndpointRouteBuilder MapDocument_РасходныйКассовыйОрдер_Endpoints(this IEndpointRouteBuilder builder)
    {
        string feature = nameof(Document_РасходныйКассовыйОрдер);

        var apiGroup = builder.MapGroup("/api/1c/ut/document-расходный-кассовый-ордер")
            .WithTags($"1C UT {feature}");

        apiGroup.MapGet("/", GetDocument_РасходныйКассовыйОрдер_ByDate)
            .WithName($"Get{feature}_ByDate")
            .WithSummary($"Get{feature}_ByDate")
            .WithDescription($"Get {feature} by Date from DB");

        apiGroup.MapGet("/{refKey}", GetDocument_РасходныйКассовыйОрдер_ByRefKey)
            .WithName($"Get{feature}_ByRefKey")
            .WithSummary($"Get{feature}_ByRefKey")
            .WithDescription($"Get {feature} By Date from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/document-расходный-кассовый-ордер")
            .WithTags($"1C UT {feature}");

        extGroup.MapPut("/resync", ResyncDocument_РасходныйКассовыйОрдер_ByDate)
            .WithName($"Resync{feature}_ByDate")
            .WithSummary($"Resync{feature}_ByDate")
            .WithDescription($"Resync {feature} from OneS Ut and save to DB");

        extGroup.MapPatch("/notify", DocumentPublisher.PublishAsync<Document_РасходныйКассовыйОрдер>)
            .WithName($"Notify{feature}")
            .WithSummary($"Notify{feature}")
            .WithDescription($"Notify {feature}");

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

    private static async Task<Results<Ok, BadRequest>> ResyncDocument_РасходныйКассовыйОрдер_ByDate(
        [FromServices] IDocument_РасходныйКассовыйОрдер_Service documentService,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        await documentService.ResyncAsync(from, to);

        return TypedResults.Ok();
    }
}
