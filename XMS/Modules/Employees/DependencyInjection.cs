using XMS.Modules.Employees.Abstractions;
using XMS.Modules.Employees.Application;

namespace XMS.Modules.Employees
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmployeesModule(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserAdService, UserAdService>();
            services.AddScoped<IUserUtService, UserUtService>();
            services.AddScoped<IEmployeeBuhService, EmployeeBuhService>();
            services.AddScoped<IEmployeeZupService, EmployeeZupService>();
            services.AddScoped<IJobTitleService, JobTitleService>();

            return services;
        }
    }
}
