using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;

public static class Catalog_Пользователи_Endpoints
{
    public static IEndpointRouteBuilder MapCatalog_Пользователи_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/catalog-пользователи")
            .WithTags("1C UT Catalog_Пользователи");

        apiGroup.MapGet("/{refKey}", GetCatalog_Пользователи_ByRefKey)
            .WithName(nameof(GetCatalog_Пользователи_ByRefKey))
            .WithSummary(nameof(GetCatalog_Пользователи_ByRefKey))
            .WithDescription("Get Catalog_Пользователи By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_Пользователи_List)
            .WithName(nameof(GetCatalog_Пользователи_List))
            .WithSummary(nameof(GetCatalog_Пользователи_List))
            .WithDescription("Get Catalog_Пользователи List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-пользователи")
            .WithTags("1C UT Catalog_Пользователи");

        extGroup.MapPatch("/notify", NotifyCatalog_Пользователи)
            .WithName(nameof(NotifyCatalog_Пользователи))
            .WithSummary(nameof(NotifyCatalog_Пользователи))
            .WithDescription("Notify Catalog_Пользователи");

        extGroup.MapPut("/resync", ResyncCatalog_Пользователи)
            .WithName(nameof(ResyncCatalog_Пользователи))
            .WithSummary(nameof(ResyncCatalog_Пользователи))
            .WithDescription("Resync Catalog_Пользователи from OneS Ut and save to DB");

        return builder;
    }
    private static async Task<Results<Ok<Catalog_Пользователи>, NotFound>> GetCatalog_Пользователи_ByRefKey(HttpContext context,
        [FromServices] ICatalog_Пользователи_Service catalogService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await catalogService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Catalog_Пользователи>>, BadRequest>> GetCatalog_Пользователи_List(HttpContext context,
        [FromServices] ICatalog_Пользователи_Service catalogService,
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

    private static async Task<Results<Ok, BadRequest<string>>> ResyncCatalog_Пользователи(HttpContext httpContext,
        [FromServices] ICatalog_Пользователи_Service catalogService)
    {
        await catalogService.ResyncAsync();

        return TypedResults.Ok();
    }

    private static async Task<IResult> NotifyCatalog_Пользователи(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] CatalogEvent catalogEvent)
    {
        await publisher.PublishAsync(Catalog_Пользователи.GetExchangeName(hostEnvironment), catalogEvent);

        return TypedResults.Ok();
    }
}
