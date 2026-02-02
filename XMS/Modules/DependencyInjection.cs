using XMS.Modules.Costs;
using XMS.Modules.Employees;

namespace XMS.Modules
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEmployeesModule();
            services.AddCostsModule();

            return services;
        }
    }
}
