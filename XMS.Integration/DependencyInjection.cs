using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XMS.Integration.Abstractions;
using XMS.Integration.AD;
using XMS.Integration.Bitrix;
using XMS.Integration.OneC;

namespace XMS.Integration;

public static class DependencyInjection
{
    public static IServiceCollection AddAppIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOneCServices(configuration);
        services.AddAdServices(configuration);
        services.AddBitrixServices(configuration);

        return services;
    }

    public static List<Type> AddAppIntegrationEventHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            return new List<Type>();
        }

        var handlerMappings = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>))
                .Select(i => new { Interface = i, Implementation = t }))
            .ToList();

        foreach (var map in handlerMappings)
        {
            services.AddScoped(map.Interface, map.Implementation);
        }

        return handlerMappings
            .Select(m => m.Interface)
            .Distinct()
            .ToList(); ;
    }

    public static IEndpointRouteBuilder MapIntegrationEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapOneCEndpoints();

        return builder;
    }
}
