using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using XMS.Application.Abstractions.Services;
using XMS.Application.Common;
using XMS.Application.Endpoints.Dto;
using XMS.Domain.Models;

namespace XMS.Application.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static IEndpointRouteBuilder MapEmployeeEndpints(this IEndpointRouteBuilder routeBuilder)
        {
            var employeeGroup = routeBuilder.MapGroup("/api/employees")
                .WithTags("XMS Employees")
                .AddEndpointFilter<ApiKeyAuthFilter>();

            employeeGroup.MapGet("/", GetEmployeeList).WithName(nameof(GetEmployeeList));
            employeeGroup.MapGet("/{id}", GetEmployeeById).WithName(nameof(GetEmployeeById));
            employeeGroup.MapGet("/by-ut-refkey/{refKey}", GetEmployeeByUtRefKey).WithName(nameof(GetEmployeeByUtRefKey));
            employeeGroup.MapGet("/by-ad-login/{login}", GetEmployeeByAdLogin).WithName(nameof(GetEmployeeByAdLogin));
            employeeGroup.MapGet("/by-manager-id/{id}", GetEmployeesByManagerId).WithName(nameof(GetEmployeesByManagerId));
            employeeGroup.MapGet("/managers-by-employee-id/{id}",GetManagersByEmployeeId).WithName(nameof(GetManagersByEmployeeId));

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

        static async Task<IResult> GetManagersByEmployeeId(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            IReadOnlyList<Employee> managers = await service.GetManagersByEmployeeIdAsync(id);

            return TypedResults.Ok(managers.Select(e => e.Id));
        }

        static async Task<IResult> GetEmployeesByManagerId(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            IReadOnlyList<Employee> employees = await service.GetEmployeesByManagerIdAsync(id);

            return TypedResults.Ok(employees.Select(e => e.Id));
        }
    }
}
