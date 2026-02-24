using XMS.Application.Abstractions.Integration;

namespace XMS.Api.Endpoints
{
    public static class YuNuEndpoints
    {
        public static IEndpointRouteBuilder MapYuNuEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/integration/yunu/article-relations/godoo", async (IYuNuService service, CancellationToken ct) =>
                {
                    var result = await service.GetArticleRelationsAsync("godoo", ct);
                    return Results.Ok(result);
                })
                .WithName("GetYuNuArticleRelations_godoo")
                .WithTags("YuNu");

            endpoints.MapGet("/integration/yunu/article-relations/ip", async (IYuNuService service, CancellationToken ct) =>
            {
                var result = await service.GetArticleRelationsAsync("ip", ct);
                return Results.Ok(result);
            })
                .WithName("GetYuNuArticleRelations_ip")
                .WithTags("YuNu");

            return endpoints;
        }
    }
}
