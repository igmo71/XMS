using XMS.Modules.Costs.Abstractions;
using XMS.Modules.Costs.Application;

namespace XMS.Modules.Costs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCostsModule(this IServiceCollection services)
        {
            services.AddScoped<ICostService, CostService>();

            return services;
        }
    }
}
