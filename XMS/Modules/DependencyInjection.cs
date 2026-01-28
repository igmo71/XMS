using XMS.Modules.Employees;

namespace XMS.Modules
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEmployeesModule();

            return services;
        }
    }
}
