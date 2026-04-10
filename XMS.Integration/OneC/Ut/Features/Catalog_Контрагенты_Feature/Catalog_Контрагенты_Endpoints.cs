using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

public static class Catalog_Контрагенты_Endpoints
{

    public static IEndpointRouteBuilder MapCatalog_Контрагенты_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/catalog-контрагенты")
           .WithTags("1C UT Catalog_Контрагенты");

        apiGroup.MapGet("/{refKey}", GetCatalog_Контрагенты_ByRefKey)
            .WithName(nameof(GetCatalog_Контрагенты_ByRefKey))
            .WithSummary(nameof(GetCatalog_Контрагенты_ByRefKey))
            .WithDescription("Get Catalog_Контрагенты By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_Контрагенты_List)
            .WithName(nameof(GetCatalog_Контрагенты_List))
            .WithSummary(nameof(GetCatalog_Контрагенты_List))
            .WithDescription("Get Catalog_Контрагенты List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-контрагенты")
            .WithTags("1C UT Catalog_Контрагенты");

        extGroup.MapPatch("/notify", NotifyCatalog_Контрагенты)
            .WithName(nameof(NotifyCatalog_Контрагенты))
            .WithSummary(nameof(NotifyCatalog_Контрагенты))
            .WithDescription("Notify Catalog_Контрагенты");

        extGroup.MapPut("/resync", ResyncCatalog_Контрагенты)
            .WithName(nameof(ResyncCatalog_Контрагенты))
            .WithSummary(nameof(ResyncCatalog_Контрагенты))
            .WithDescription("Resync Catalog_Контрагенты from OneS Ut and save to DB");

        return builder;
    }

    private static async Task<Results<Ok<Catalog_Контрагенты>, NotFound>> GetCatalog_Контрагенты_ByRefKey(HttpContext context,
        [FromServices] ICatalog_Контрагенты_Service catalogService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await catalogService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Catalog_Контрагенты>>, BadRequest>> GetCatalog_Контрагенты_List(HttpContext context,
        [FromServices] ICatalog_Контрагенты_Service catalogService,
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

    private static async Task<Results<Ok, BadRequest<string>>> ResyncCatalog_Контрагенты(HttpContext httpContext,
        [FromServices] ICatalog_Контрагенты_Service catalogService)
    {
        await catalogService.ResyncAsync();

        return TypedResults.Ok();
    }

    private static async Task<IResult> NotifyCatalog_Контрагенты(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] CatalogEvent catalogEvent)
    {
        await publisher.PublishAsync(Catalog_Контрагенты.GetExchangeName(hostEnvironment), catalogEvent);

        return TypedResults.Ok();
    }
}
