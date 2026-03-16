using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Text.Json;
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
                .AddEndpointFilter<ApiKeyAuthFilter>()
                .ProducesProblem(401)
                .ProducesValidationProblem();

            employeeGroup.MapGet("/", GetEmployeeList).WithName(nameof(GetEmployeeList));
            employeeGroup.MapGet("/{id}", GetEmployeeById).WithName(nameof(GetEmployeeById));
            employeeGroup.MapGet("/by-ut-refkey/{utRefKey}", GetEmployeeByUtRefKey).WithName(nameof(GetEmployeeByUtRefKey));
            employeeGroup.MapGet("/by-ad-login/{login}", GetEmployeeByAdLogin).WithName(nameof(GetEmployeeByAdLogin));
            //employeeGroup.MapGet("/by-manager-id/{id}", GetEmployeesByManagerId).WithName(nameof(GetEmployeesByManagerId));
            employeeGroup.MapGet("/managers-by-employee-id/{id}", GetManagersByEmployeeId).WithName(nameof(GetManagersByEmployeeId));

            routeBuilder.MapPost("/api/test-body", (HttpContext httpContext, ILogger<ServiceResult> logger , [FromBody] JsonElement testBody) =>
            {
                logger.LogDebug("testBody {testBody}", testBody);
                return TypedResults.Ok( testBody);
            });

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

        static async Task<Results<Ok<Employee>, NotFound<Guid>>> GetEmployeeById(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            var clientName = httpContext.Items["ClientName"];

            var result = await service.GetByIdAsync(id);
            return result is null ? TypedResults.NotFound(id) : TypedResults.Ok(result);
        }

        static async Task<Results<Ok<EmployeeDto>, NotFound<string>>> GetEmployeeByUtRefKey(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] string utRefKey)
        {
            var employee = await service.GetByUtRefKeyAsync(utRefKey);
            return employee is null ? TypedResults.NotFound($"No Employee fount by Ut User Ref_Key {utRefKey}") : TypedResults.Ok(EmployeeDto.From(employee));
        }

        static async Task<Results<Ok<EmployeeDto>, NotFound<string>>> GetEmployeeByAdLogin(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] string login)
        {
            var employee = await service.GetByAdLoginAsync(login);
            return employee is null ? TypedResults.NotFound($"No Employee fount by AD User Login: {login}") : TypedResults.Ok(EmployeeDto.From(employee));
        }

        static async Task<Ok<IEnumerable<Guid>>> GetManagersByEmployeeId(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            IReadOnlyList<Employee> managers = await service.GetManagersByEmployeeIdAsync(id);

            return TypedResults.Ok(managers.Select(e => e.Id));
        }

        static async Task<Ok<IEnumerable<Guid>>> GetEmployeesByManagerId(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            IReadOnlyList<Employee> employees = await service.GetEmployeesByManagerIdAsync(id);

            return TypedResults.Ok(employees.Select(e => e.Id));
        }
    }
}
