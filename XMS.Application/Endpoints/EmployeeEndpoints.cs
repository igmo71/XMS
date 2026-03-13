using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;
using XMS.Application.Endpoints.Dto;

namespace XMS.Application.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static IEndpointRouteBuilder MapEmployeeEndpints(this IEndpointRouteBuilder routeBuilder)
        {
            var employeeGroup = routeBuilder.MapGroup("/api/employees")
                .WithTags("XMS.Api.Employee")
                .AddEndpointFilter<ApiKeyAuthFilter>();

            employeeGroup.MapGet("/", GetEmployeeList).WithName(nameof(GetEmployeeList));
            employeeGroup.MapGet("/{id}", GetEmployeeById).WithName(nameof(GetEmployeeById));
            employeeGroup.MapGet("/by-ut-refkey/{refKey}", GetEmployeeByUtRefKey).WithName(nameof(GetEmployeeByUtRefKey));
            employeeGroup.MapGet("/by-ad-login/{login}", GetEmployeeByAdLogin).WithName(nameof(GetEmployeeByAdLogin));

            return routeBuilder;
        }

        static async Task<IResult> GetEmployeeList(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromQuery] int? skip,
            [FromQuery] int? take,
            [FromQuery] bool? includeDeleted)
        {
            var clientName = httpContext.Items["ClientName"];
            var result = await service.GetListAsync(new QueryParameters(skip, take, includeDeleted));
            return TypedResults.Ok(result);
        }

        static async Task<IResult> GetEmployeeById(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            var clientName = httpContext.Items["ClientName"];
            var result = await service.GetByIdAsync(id);
            return TypedResults.Ok(result);
        }

        static async Task<IResult> GetEmployeeByUtRefKey(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] string refKey)
        {
            var employee = await service.GetByUtRefKeyAsync(refKey);
            return employee is null ? TypedResults.NotFound() : TypedResults.Ok(EmployeeDto.From(employee));
        }

        static async Task<IResult> GetEmployeeByAdLogin(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] string login)
        {
            var employee = await service.GetByAdLoginAsync(login);
            return employee is null ? TypedResults.NotFound() : TypedResults.Ok(EmployeeDto.From(employee));
        }
    }
}
