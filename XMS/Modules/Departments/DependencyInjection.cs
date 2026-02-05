using XMS.Modules.Departments.Abstractions;
using XMS.Modules.Departments.Application;

namespace XMS.Modules.Departments
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
