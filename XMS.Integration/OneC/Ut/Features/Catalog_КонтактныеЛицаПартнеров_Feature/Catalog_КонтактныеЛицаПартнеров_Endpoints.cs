using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

public static class Catalog_КонтактныеЛицаПартнеров_Endpoints
{
    public static IEndpointRouteBuilder Map_Catalog_КонтактныеЛицаПартнеров_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/catalog_контактные-лица-партнеров")
           .WithTags("1C UT Catalog_КонтактныеЛицаПартнеров");

        apiGroup.MapGet("/{refKey}", GetCatalog_КонтактныеЛицаПартнеров_ByRefKey)
            .WithName(nameof(GetCatalog_КонтактныеЛицаПартнеров_ByRefKey))
            .WithSummary(nameof(GetCatalog_КонтактныеЛицаПартнеров_ByRefKey))
            .WithDescription("Get Catalog_КонтактныеЛицаПартнеров By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_КонтактныеЛицаПартнеров_List)
            .WithName(nameof(GetCatalog_КонтактныеЛицаПартнеров_List))
            .WithSummary(nameof(GetCatalog_КонтактныеЛицаПартнеров_List))
            .WithDescription("Get Catalog_КонтактныеЛицаПартнеров List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-контактные-лица-партнеров")
            .WithTags("1C UT Catalog_КонтактныеЛицаПартнеров");

        extGroup.MapPatch("/notify", NotifyCatalog_КонтактныеЛицаПартнеров)
            .WithName(nameof(NotifyCatalog_КонтактныеЛицаПартнеров))
            .WithSummary(nameof(NotifyCatalog_КонтактныеЛицаПартнеров))
            .WithDescription("Notify Catalog_КонтактныеЛицаПартнеров");

        extGroup.MapPut("/resync", ResyncCatalog_КонтактныеЛицаПартнеров)
            .WithName(nameof(ResyncCatalog_КонтактныеЛицаПартнеров))
            .WithSummary(nameof(ResyncCatalog_КонтактныеЛицаПартнеров))
            .WithDescription("Resync Catalog_КонтактныеЛицаПартнеров from OneS Ut and save to DB");

        return builder;
    }

    private static async Task<Results<Ok<Catalog_КонтактныеЛицаПартнеров>, NotFound>> GetCatalog_КонтактныеЛицаПартнеров_ByRefKey(HttpContext context,
        [FromServices] ICatalog_КонтактныеЛицаПартнеров_Service catalogService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await catalogService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Catalog_КонтактныеЛицаПартнеров>>, BadRequest>> GetCatalog_КонтактныеЛицаПартнеров_List(HttpContext context,
        [FromServices] ICatalog_КонтактныеЛицаПартнеров_Service catalogService,
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

    private static async Task<Results<Ok, BadRequest<string>>> ResyncCatalog_КонтактныеЛицаПартнеров(HttpContext httpContext,
        [FromServices] ICatalog_КонтактныеЛицаПартнеров_Service catalogService)
    {
        await catalogService.ResyncAsync();

        return TypedResults.Ok();
    }

    private static async Task<IResult> NotifyCatalog_КонтактныеЛицаПартнеров(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] CatalogEvent catalogEvent)
    {
        await publisher.PublishAsync(Catalog_КонтактныеЛицаПартнеров.GetExchangeName(hostEnvironment), catalogEvent);

        return TypedResults.Ok();
    }
}
