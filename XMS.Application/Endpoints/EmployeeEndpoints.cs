using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;

namespace XMS.Application.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static IEndpointRouteBuilder MapEmployeeEndpints(this IEndpointRouteBuilder routeBuilder)
        {
            var employeesGroup = routeBuilder.MapGroup("/api/employees").WithTags("XMS");

            employeesGroup.MapGet("/", GetEmployeeList).WithName(nameof(GetEmployeeList));
            employeesGroup.MapGet("/{id}", GetEmployeeById).WithName(nameof(GetEmployeeById));

            return routeBuilder;
        }

        static async Task<IResult> GetEmployeeList(
            [FromServices] IEmployeeService service,
            [FromQuery] int? skip,
            [FromQuery] int? take,
            [FromQuery] bool? includeDeleted)
        {
            var result = await service.GetListAsync(new QueryParameters(skip, take, includeDeleted));
            return TypedResults.Ok(result);
        }

        static async Task<IResult> GetEmployeeById(
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            var result = await service.GetByIdAsync(id);
            return TypedResults.Ok(result);
        }
    }
}
