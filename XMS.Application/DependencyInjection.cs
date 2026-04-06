using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using XMS.Application.Abstractions.Services;
using XMS.Application.Endpoints;
using XMS.Application.Services;

namespace XMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeBuhService, EmployeeBuhService>();
        services.AddScoped<IEmployeeZupService, EmployeeZupService>();
        services.AddScoped<IJobTitleService, JobTitleService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IUserAdService, UserAdService>();
        services.AddScoped<IUserUtService, UserUtService>();

        return services;
    }

    public static IEndpointRouteBuilder MapApplicationEndpints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapTestBodyEndpint();
        routeBuilder.MapEmployeeEndpints();

        return routeBuilder;
    }
}
