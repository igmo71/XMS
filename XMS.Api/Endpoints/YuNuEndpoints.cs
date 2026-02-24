using XMS.Application.Abstractions.Integration;

namespace XMS.Api.Endpoints
{
    public static class YuNuEndpoints
    {
        public static IEndpointRouteBuilder MapYuNuEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/integration/yunu/article-relations", async (IYuNuService service, CancellationToken ct) =>
                {
                    var result = await service.GetArticleRelationsAsync(ct);
                    return Results.Ok(result);
                })
                .WithName("GetYuNuArticleRelations")
                .WithTags("YuNu");

            return endpoints;
        }
    }
}
