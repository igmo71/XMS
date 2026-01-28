using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Application;

namespace XMS.Modules.Employees
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmployeesModule(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserUtService, UserUtService>();

            return services;
        }
    }
}
