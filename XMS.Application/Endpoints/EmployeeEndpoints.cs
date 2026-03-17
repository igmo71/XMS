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

            employeeGroup.MapGet("/", GetEmployeeList)
                .WithName(nameof(GetEmployeeList))
                .WithSummary(nameof(GetEmployeeList))
                .WithDescription("Демо - Получить список всех Сотрудников со всеми зависимостями");
            employeeGroup.MapGet("/{id}", GetEmployeeById)
                .WithName(nameof(GetEmployeeById))
                .WithSummary(nameof(GetEmployeeById))
                .WithDescription("Получить Сотрудника по его Id");
            employeeGroup.MapGet("/by-ut-refkey/{utRefKey}", GetEmployeeByUtRefKey)
                .WithName(nameof(GetEmployeeByUtRefKey))
                .WithSummary(nameof(GetEmployeeByUtRefKey))
                .WithDescription("Получить Сотрудника по его Ref_Key пользователя 1С УТ");
            employeeGroup.MapGet("/by-ad-login/{login}", GetEmployeeByAdLogin)
                .WithName(nameof(GetEmployeeByAdLogin))
                .WithSummary(nameof(GetEmployeeByAdLogin))
                .WithDescription("Получить Сотрудника по его логину в AD");
            employeeGroup.MapGet("/by-manager-id/{id}", GetEmployeesByManagerId)
                .WithName(nameof(GetEmployeesByManagerId))
                .WithSummary(nameof(GetEmployeesByManagerId))
                .WithDescription("По Id Сотрудника получить список Id всех его подчиненных (может сломаться, если ссылки зациклены)");
            employeeGroup.MapGet("/managers-by-employee-id/{id}", GetManagersByEmployeeId)
                .WithName(nameof(GetManagersByEmployeeId))
                .WithSummary(nameof(GetManagersByEmployeeId))
                .WithDescription("По Id Сотрудника получить список Id всех его менеджеров");

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

        static async Task<Results<Ok<IEnumerable<Guid>>, ProblemHttpResult>> GetManagersByEmployeeId(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            var result = await service.GetManagersByEmployeeIdAsync(id);

            return result.IsSuccess
                    ? TypedResults.Ok(result.Value.Select(e => e.Id))
                    : TypedResults.Problem(detail: result.Error.Description, title: result.Error.Name);
        }

        static async Task<Results<Ok<IEnumerable<Guid>>, ProblemHttpResult>> GetEmployeesByManagerId(HttpContext httpContext,
            [FromServices] IEmployeeService service,
            [FromRoute] Guid id)
        {
            var result = await service.GetEmployeesByManagerIdAsync(id);

            return result.IsSuccess
                    ? TypedResults.Ok(result.Value.Select(e => e.Id))
                    : TypedResults.Problem(detail: result.Error.Description, title: result.Error.Name);
        }
    }
}
