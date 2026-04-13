using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Core;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;

public static class Catalog_Контрагенты_Endpoints
{

    public static IEndpointRouteBuilder MapCatalog_Контрагенты_Endpoints(this IEndpointRouteBuilder builder)
    {
        string feature = nameof(Catalog_Контрагенты);

        var apiGroup = builder.MapGroup("/api/1c/ut/catalog-контрагенты")
            .WithTags($"1C UT {feature}");

        apiGroup.MapGet("/{refKey}", GetCatalog_Контрагенты_ByRefKey)
            .WithName($"Get{feature}_ByRefKey")
            .WithSummary($"Get{feature}_ByRefKey")
            .WithDescription($"Get {feature} By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_Контрагенты_List)
            .WithName($"Get{feature}_List")
            .WithSummary($"Get{feature}_List")
            .WithDescription($"Get {feature} List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-контрагенты")
            .WithTags($"1C UT {feature}");

        extGroup.MapPut("/resync", ResyncCatalog_Контрагенты)
            .WithName($"Resync{feature}")
            .WithSummary($"Resync{feature}")
            .WithDescription($"Resync {feature} from OneS Ut and save to DB");

        extGroup.MapPatch("/notify", IntegrationEventPublisher.Publish<Catalog_Контрагенты>)
            .WithName($"Notify{feature}")
            .WithSummary($"Notify{feature}")
            .WithDescription($"Notify {feature}");

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

    //private static async Task<IResult> NotifyCatalog_Контрагенты(HttpContext httpContext,
    //    [FromServices] IEventPublisher publisher,
    //    [FromServices] IHostEnvironment hostEnvironment,
    //    [FromBody] CatalogEvent catalogEvent)
    //{
    //    await publisher.PublishAsync(Catalog_Контрагенты.GetExchangeName(hostEnvironment), catalogEvent);

    //    return TypedResults.Ok();
    //}
}
