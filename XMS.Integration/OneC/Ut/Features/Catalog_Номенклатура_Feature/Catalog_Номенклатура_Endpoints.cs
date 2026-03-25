using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

public static class Catalog_Номенклатура_Endpoints
{
    public static IEndpointRouteBuilder Map_Catalog_Номенклатура_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/catalog-номенклатура")
           .WithTags("1C UT Catalog_Номенклатура");

        apiGroup.MapGet("/{refKey}", GetCatalog_Номенклатура_ByRefKey)
            .WithName(nameof(GetCatalog_Номенклатура_ByRefKey))
            .WithSummary(nameof(GetCatalog_Номенклатура_ByRefKey))
            .WithDescription("Get Catalog_Номенклатура By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_Номенклатура_List)
            .WithName(nameof(GetCatalog_Номенклатура_List))
            .WithSummary(nameof(GetCatalog_Номенклатура_List))
            .WithDescription("Get Catalog_Номенклатура List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-номенклатура")
            .WithTags("1C UT Catalog_Номенклатура");

        extGroup.MapPut("/resync", ResyncCatalog_Номенклатура)
            .WithName(nameof(ResyncCatalog_Номенклатура))
            .WithSummary(nameof(ResyncCatalog_Номенклатура))
            .WithDescription("Resync Catalog_Номенклатура from OneS Ut and save to DB");

        extGroup.MapPatch("/notify", NotifyCatalog_Номенклатура)
            .WithName(nameof(NotifyCatalog_Номенклатура))
            .WithSummary(nameof(NotifyCatalog_Номенклатура))
            .WithDescription("Notify Catalog_Номенклатура");

        return builder;
    }

    private static async Task<Results<Ok<Catalog_Номенклатура>, NotFound>> GetCatalog_Номенклатура_ByRefKey(HttpContext context,
        [FromServices] ICatalog_Номенклатура_Service catalogService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await catalogService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Catalog_Номенклатура>>, BadRequest>> GetCatalog_Номенклатура_List(HttpContext context,
        [FromServices] ICatalog_Номенклатура_Service catalogService,
        [FromQuery] string? descriptionTerm = null,
        [FromQuery] bool includeDeleted = false,
        [FromQuery] int? skip = AppSettings.Default.Skip,
        [FromQuery] int? take = AppSettings.Default.Take,
        CancellationToken ct = default)
    {
        var parameters = new CatalogQueryParameters(descriptionTerm, includeDeleted, skip, take);

        var result = await catalogService.GetListAsync(parameters, ct);

        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok, BadRequest<string>>> ResyncCatalog_Номенклатура(HttpContext httpContext,
        [FromServices] ICatalog_Номенклатура_Service catalogService)
    {
        await catalogService.ResyncAsync();

        return TypedResults.Ok();
    }

    private static async Task<IResult> NotifyCatalog_Номенклатура(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromBody] Catalog_Номенклатура_Changed oneCNotifyMessage)
    {
        await publisher.PublishAsync(Catalog_Номенклатура.GetExchangeName(), oneCNotifyMessage);

        return TypedResults.Ok();
    }
}
