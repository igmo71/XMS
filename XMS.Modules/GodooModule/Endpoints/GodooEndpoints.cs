using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Modules.GodooModule.Abstractions;

namespace XMS.Modules.GodooModule.Endpoints
{
    public static class GodooEndpoints
    {
        public static IEndpointRouteBuilder MapGodooEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/integration/godoo/reload", async ([FromServices] IGodooService service, CancellationToken ct) =>
            {
                await service.Reload("godoo", ct);
                return Results.NoContent();
            })
                .WithName("ReloadGodoo")
                .WithTags("Godoo");

            endpoints.MapGet("/integration/yunu/article-relations/godoo", async ([FromServices] IYunuService service, CancellationToken ct) =>
            {
                var result = await service.GetArticleRelationsAsync("godoo", ct);
                return Results.Ok(result);
            })
                .WithName("GetYunuArticleRelations_godoo")
                .WithTags("Yunu");

            endpoints.MapGet("/integration/yunu/article-relations/ip", async ([FromServices] IYunuService service, CancellationToken ct) =>
            {
                var result = await service.GetArticleRelationsAsync("ip", ct);
                return Results.Ok(result);
            })
                .WithName("GetYunuArticleRelations_ip")
                .WithTags("Yunu");

            return endpoints;
        }
    }
}
