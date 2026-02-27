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
            // Godoo
            var godooGroup = endpoints.MapGroup("/api/godoo").WithTags("Godoo");

            godooGroup.MapGet("/marketplace-relations/reload/godoo", async (
                [FromServices] IGodooService service, CancellationToken ct) =>
            {
                await service.Reload("godoo", ct);
                return Results.NoContent();
            }).WithName("ReloadGodoo");

            godooGroup.MapGet("/marketplace-relations/reload/ip", async (
                [FromServices] IGodooService service, CancellationToken ct) =>
            {
                await service.Reload("ip", ct);
                return Results.NoContent();
            }).WithName("ReloadIp");

            // Yunu
            var yunuGroup = endpoints.MapGroup("/integration/yunu").WithTags("Yunu");

            yunuGroup.MapGet("/article-relations/godoo", async (
                [FromServices] IYunuService service, CancellationToken ct) =>
            {
                var result = await service.GetArticleRelationsAsync("godoo", ct);
                return Results.Ok(result);
            }).WithName("GetYunuArticleRelations_godoo");

            yunuGroup.MapGet("/article-relations/ip", async (
                [FromServices] IYunuService service, CancellationToken ct) =>
            {
                var result = await service.GetArticleRelationsAsync("ip", ct);
                return Results.Ok(result);
            }).WithName("GetYunuArticleRelations_ip");

            return endpoints;
        }
    }
}
