using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace XMS.Modules.CostModule.Endpoints;

public static class CostEndpoints
{
    public static IEndpointRouteBuilder MapCostEndpoints(this IEndpointRouteBuilder builder)
    {
        var costGroup = builder.MapGroup("/api/cost")
            .WithTags("XMS Cost")
            //.AddEndpointFilter<ApiKeyAuthFilter>()
            //.ProducesProblem(401)
            .ProducesValidationProblem();

        return builder;
    }
}
