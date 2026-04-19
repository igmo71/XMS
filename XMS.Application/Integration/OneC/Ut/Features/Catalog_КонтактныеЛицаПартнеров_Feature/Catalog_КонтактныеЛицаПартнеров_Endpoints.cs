using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;

public static class Catalog_КонтактныеЛицаПартнеров_Endpoints
{
    public static IEndpointRouteBuilder MapCatalog_КонтактныеЛицаПартнеров_Endpoints(this IEndpointRouteBuilder builder)
    {
        string feature = nameof(Catalog_КонтактныеЛицаПартнеров);

        var apiGroup = builder.MapGroup("/api/1c/ut/catalog_контактные-лица-партнеров")
            .WithTags($"1C UT {feature}");

        apiGroup.MapGet("/{refKey}", GetCatalog_КонтактныеЛицаПартнеров_ByRefKey)
            .WithName($"Get{feature}_ByRefKey")
            .WithSummary($"Get{feature}_ByRefKey")
            .WithDescription($"Get {feature} By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_КонтактныеЛицаПартнеров_List)
            .WithName($"Get{feature}_List")
            .WithSummary($"Get{feature}_List")
            .WithDescription($"Get {feature} List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-контактные-лица-партнеров")
            .WithTags($"1C UT {feature}");

        extGroup.MapPut("/resync", ResyncCatalog_КонтактныеЛицаПартнеров)
            .WithName($"Resync{feature}")
            .WithSummary($"Resync{feature}")
            .WithDescription($"Resync {feature} from OneS Ut and save to DB");

        extGroup.MapPatch("/notify", CatalogPublisher.PublishAsync<Catalog_КонтактныеЛицаПартнеров_Notification>)
            .WithName($"Notify{feature}")
            .WithSummary($"Notify{feature}")
            .WithDescription($"Notify {feature}");

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
}
