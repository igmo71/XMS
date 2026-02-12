using XMS.Web.Modules.Costs;
using XMS.Web.Modules.Departments;
using XMS.Web.Modules.Employees;

namespace XMS.Web.Modules
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
