using XMS.Web.Modules.Departments.Abstractions;
using XMS.Web.Modules.Departments.Application;

namespace XMS.Web.Modules.Departments
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDepartmentsModule(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}
