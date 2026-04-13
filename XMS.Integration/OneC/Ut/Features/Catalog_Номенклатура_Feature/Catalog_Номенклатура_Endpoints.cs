using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Core;
using XMS.Integration.OneC.Common;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

public static class Catalog_Номенклатура_Endpoints
{
    public static IEndpointRouteBuilder MapCatalog_Номенклатура_Endpoints(this IEndpointRouteBuilder builder)
    {
        string feature = nameof(Catalog_Номенклатура);

        var apiGroup = builder.MapGroup("/api/1c/ut/catalog-номенклатура")
            .WithTags($"1C UT {feature}");

        apiGroup.MapGet("/{refKey}", GetCatalog_Номенклатура_ByRefKey)
            .WithName($"Get{feature}_ByRefKey")
            .WithSummary($"Get{feature}_ByRefKey")
            .WithDescription($"Get {feature} By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_Номенклатура_List)
            .WithName($"Get{feature}_List")
            .WithSummary($"Get{feature}_List")
            .WithDescription($"Get {feature} List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-номенклатура")
            .WithTags($"1C UT {feature}");

        extGroup.MapPut("/resync", ResyncCatalog_Номенклатура)
            .WithName($"Resync{feature}")
            .WithSummary($"Resync{feature}")
            .WithDescription($"Resync {feature} from OneS Ut and save to DB");

        extGroup.MapPatch("/notify", IntegrationEventPublisher.Publish<Catalog_Номенклатура>)
            .WithName($"Notify{feature}")
            .WithSummary($"Notify{feature}")
            .WithDescription($"Notify {feature}");

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
}
