using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Integration.AD;
using XMS.Integration.Bitrix;
using XMS.Integration.OneC.Common;

namespace XMS.Integration;

public static class DependencyInjection
{
    public static IServiceCollection AddIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOneCServices(configuration);
        services.AddAdServices(configuration);
        services.AddBitrixServices(configuration);

        return services;
    }

    public static IEndpointRouteBuilder MapIntegrationEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapOneCEndpoints();

        return builder;
    }
}
