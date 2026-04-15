using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Core;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;

public static class Catalog_Партнеры_Endpoints
{
    public static IEndpointRouteBuilder MapCatalog_Партнеры_Endpoints(this IEndpointRouteBuilder builder)
    {
        var apiGroup = builder.MapGroup("/api/1c/ut/catalog-партнеры")
            .WithTags("1C UT Catalog_Партнеры");

        apiGroup.MapGet("/{refKey}", GetCatalog_Партнеры_ByRefKey)
            .WithName(nameof(GetCatalog_Партнеры_ByRefKey))
            .WithSummary(nameof(GetCatalog_Партнеры_ByRefKey))
            .WithDescription("Get Catalog_Партнеры By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_Партнеры_List)
            .WithName(nameof(GetCatalog_Партнеры_List))
            .WithSummary(nameof(GetCatalog_Партнеры_List))
            .WithDescription("Get Catalog_Партнеры List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-партнеры")
            .WithTags("1C UT Catalog_Партнеры");

        extGroup.MapPut("/resync", ResyncCatalog_Партнеры)
            .WithName(nameof(ResyncCatalog_Партнеры))
            .WithSummary(nameof(ResyncCatalog_Партнеры))
            .WithDescription("Resync Catalog_Партнеры from OneS Ut and save to DB");

        extGroup.MapPatch("/notify", IntegrationEventPublisher.PublishCatalogNotificationAsync<Catalog_Партнеры>)
            .WithName("NotifyCatalog_Партнеры")
            .WithSummary("NotifyCatalog_Партнеры")
            .WithDescription("Notify Catalog_Партнеры");

        return builder;
    }

    private static async Task<Results<Ok<Catalog_Партнеры>, NotFound>> GetCatalog_Партнеры_ByRefKey(HttpContext context,
        [FromServices] ICatalog_Партнеры_Service catalogService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await catalogService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Catalog_Партнеры>>, BadRequest>> GetCatalog_Партнеры_List(HttpContext context,
        [FromServices] ICatalog_Партнеры_Service catalogService,
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

    private static async Task<Results<Ok, BadRequest<string>>> ResyncCatalog_Партнеры(HttpContext httpContext,
        [FromServices] ICatalog_Партнеры_Service catalogService)
    {
        await catalogService.ResyncAsync();

        return TypedResults.Ok();
    }
}
