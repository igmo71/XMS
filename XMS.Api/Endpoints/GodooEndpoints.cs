using Microsoft.AspNetCore.Mvc;
using XMS.Application.Abstractions.Services;

namespace XMS.Api.Endpoints
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
                .WithName("ReloadGoDoo")
                .WithTags("GoDoo");

            return endpoints;
        }
    }
}
