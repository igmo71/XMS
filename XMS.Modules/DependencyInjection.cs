using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Modules.CostModule;
using XMS.Modules.CostModule.Endpoints;
using XMS.Modules.GodooModule;
using XMS.Modules.GodooModule.Endpoints;

namespace XMS.Modules
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCostModule(configuration);
            services.AddGodoModule(configuration);

            return services;
        }

        public static IEndpointRouteBuilder MapModulesEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapCostEndpoints();
            endpoints.MapGodooEndpoints();

            return endpoints;
        }
    }
}
