using Microsoft.Extensions.DependencyInjection;
using XMS.Application.Abstractions.Services;
using XMS.Application.Services;

namespace XMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICostCategoryService, CostCategoryService>();
            services.AddScoped<ICostItemService, CostItemService>();
            services.AddScoped<ICostCategoryItemService, CostCategoryItemService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserAdService, UserAdService>();
            services.AddScoped<IUserUtService, UserUtService>();
            services.AddScoped<IEmployeeBuhService, EmployeeBuhService>();
            services.AddScoped<IEmployeeZupService, EmployeeZupService>();
            services.AddScoped<IJobTitleService, JobTitleService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ILocationService, LocationService>();

            return services;
        }
    }
}
