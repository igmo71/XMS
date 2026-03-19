using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using XMS.Core.Common;

namespace XMS.Application.Endpoints
{
    public static class TestBodyEndpoint
    {
        public static IEndpointRouteBuilder MapTestBodyEndpint(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("/api/test-body", (HttpContext httpContext, ILogger<ServiceResult> logger, [FromBody] JsonElement testBody) =>
            {
                logger.LogDebug("TestBody {TestBody}", testBody);

                return TypedResults.Ok(testBody);
            })
                .WithTags("XMS TestBody")
                .WithSummary("XMS TestBody")
                .WithDescription("Логирует и возвращвает тело запроса");

            return routeBuilder;
        }
    }
}
