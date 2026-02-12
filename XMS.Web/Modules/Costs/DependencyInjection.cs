using XMS.Web.Modules.Costs.Abstractions;
using XMS.Web.Modules.Costs.Application;

namespace XMS.Web.Modules.Costs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCostsModule(this IServiceCollection services)
        {
            services.AddScoped<ICostCategoryService, CostCategoryService>();
            services.AddScoped<ICostItemService, CostItemService>();
            services.AddScoped<ICostCategoryItemService, CostCategoryItemService>();

            return services;
        }
    }
}
