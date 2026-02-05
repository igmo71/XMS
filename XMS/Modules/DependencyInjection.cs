using XMS.Modules.Costs;
using XMS.Modules.Departments;
using XMS.Modules.Employees;

namespace XMS.Modules
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCostsModule();
            services.AddEmployeesModule();
            services.AddDepartmentsModule();

            return services;
        }
    }
}
