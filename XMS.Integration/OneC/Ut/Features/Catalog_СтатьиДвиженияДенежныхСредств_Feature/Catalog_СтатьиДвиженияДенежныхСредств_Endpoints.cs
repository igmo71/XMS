using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using XMS.Core;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Ut.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;

public static class Catalog_СтатьиДвиженияДенежныхСредств_Endpoints
{
    public static IEndpointRouteBuilder MapCatalog_СтатьиДвиженияДенежныхСредств_Endpoints(this IEndpointRouteBuilder builder)
    {

        var apiGroup = builder.MapGroup("/api/1c/ut/catalog-статьи-движения-денежных-средств")
           .WithTags("1C UT Catalog_СтатьиДвиженияДенежныхСредств");

        apiGroup.MapGet("/{refKey}", GetCatalog_СтатьиДвиженияДенежныхСредств_ByRefKey)
            .WithName(nameof(GetCatalog_СтатьиДвиженияДенежныхСредств_ByRefKey))
            .WithSummary(nameof(GetCatalog_СтатьиДвиженияДенежныхСредств_ByRefKey))
            .WithDescription("Get Catalog_СтатьиДвиженияДенежныхСредств By Ref_Key from DB");

        apiGroup.MapGet("/", GetCatalog_СтатьиДвиженияДенежныхСредств_List)
            .WithName(nameof(GetCatalog_СтатьиДвиженияДенежныхСредств_List))
            .WithSummary(nameof(GetCatalog_СтатьиДвиженияДенежныхСредств_List))
            .WithDescription("Get Catalog_СтатьиДвиженияДенежныхСредств List from DB");


        var extGroup = builder.MapGroup("/ext/1c/ut/catalog-статьи-движения-денежных-средств")
            .WithTags("1C UT Catalog_СтатьиДвиженияДенежныхСредств");

        extGroup.MapPatch("/notify", NotifyCatalog_СтатьиДвиженияДенежныхСредств)
            .WithName(nameof(NotifyCatalog_СтатьиДвиженияДенежныхСредств))
            .WithSummary(nameof(NotifyCatalog_СтатьиДвиженияДенежныхСредств))
            .WithDescription("Notify Catalog_СтатьиДвиженияДенежныхСредств");

        extGroup.MapPut("/resync", ResyncCatalog_СтатьиДвиженияДенежныхСредств)
            .WithName(nameof(ResyncCatalog_СтатьиДвиженияДенежныхСредств))
            .WithSummary(nameof(ResyncCatalog_СтатьиДвиженияДенежныхСредств))
            .WithDescription("Resync Catalog_СтатьиДвиженияДенежныхСредств from OneS Ut and save to DB");
        return builder;
    }


    private static async Task<Results<Ok<Catalog_СтатьиДвиженияДенежныхСредств>, NotFound>> GetCatalog_СтатьиДвиженияДенежныхСредств_ByRefKey(HttpContext context,
        [FromServices] ICatalog_СтатьиДвиженияДенежныхСредств_Service catalogService,
        [FromRoute] Guid refKey,
        CancellationToken ct = default)
    {
        var result = await catalogService.GetAsync(refKey, ct);

        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>>, BadRequest>> GetCatalog_СтатьиДвиженияДенежныхСредств_List(HttpContext context,
        [FromServices] ICatalog_СтатьиДвиженияДенежныхСредств_Service catalogService,
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

    private static async Task<Results<Ok, BadRequest<string>>> ResyncCatalog_СтатьиДвиженияДенежныхСредств(HttpContext httpContext,
        [FromServices] ICatalog_СтатьиДвиженияДенежныхСредств_Service catalogService)
    {
        await catalogService.ResyncAsync();

        return TypedResults.Ok();
    }

    private static async Task<IResult> NotifyCatalog_СтатьиДвиженияДенежныхСредств(HttpContext httpContext,
        [FromServices] IRabbitPublisher publisher,
        [FromServices] IHostEnvironment hostEnvironment,
        [FromBody] CatalogEvent catalogEvent)
    {
        await publisher.PublishAsync(Catalog_СтатьиДвиженияДенежныхСредств.GetExchangeName(hostEnvironment), catalogEvent);

        return TypedResults.Ok();
    }
}
