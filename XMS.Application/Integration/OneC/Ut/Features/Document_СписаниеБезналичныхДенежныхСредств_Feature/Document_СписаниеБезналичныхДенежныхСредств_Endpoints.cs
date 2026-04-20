using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

public static class Document_СписаниеБезналичныхДенежныхСредств_Endpoints
{
    public static IEndpointRouteBuilder MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints(this IEndpointRouteBuilder builder)
    {
        string feature = nameof(Document_СписаниеБезналичныхДенежныхСредств);

        var apiGroup = builder.MapGroup("/api/1c/ut/document-списание-безналичных-денежных-средств")
            .WithTags($"1C UT {feature}");

        apiGroup.MapGet("/", GetDocument_СписаниеБезналичныхДенежныхСредств_ByDate)
            .WithName($"Get{feature}_ByDate")
            .WithSummary($"Get{feature}_ByDate")
            .WithDescription($"Get {feature} by Date from DB");

        apiGroup.MapGet("/{refKey}", GetDocument_СписаниеБезналичныхДенежныхСредств_ByRefKey)
            .WithName($"Get{feature}_ByRefKey")
            .WithSummary($"Get{feature}_ByRefKey")
            .WithDescription($"Get {feature} By Ref_Key from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/document-списание-безналичных-денежных-средств")
            .WithTags($"1C UT {feature}");

        extGroup.MapPut("/resync", ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate)
            .WithName($"Resync{feature}_ByDate")
            .WithSummary($"Resync{feature}_ByDate")
            .WithDescription($"Resync {feature} from OneS Ut and save to DB");

        extGroup.MapPatch("/notify", DocumentPublisher.PublishAsync<Document_СписаниеБезналичныхДенежныхСредств>)
            .WithName($"Notify{feature}")
            .WithSummary($"Notify{feature}")
            .WithDescription($"Notify {feature}");

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

    private static async Task<Results<Ok, BadRequest>> ResyncDocument_СписаниеБезналичныхДенежныхСредств_ByDate(
        [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        await documentService.ResyncAsync(from, to);

        return TypedResults.Ok();
    }
}
