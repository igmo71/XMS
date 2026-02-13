using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Application.Abstractions;
using XMS.Infrastructure.Data;
using XMS.Infrastructure.Integration;

namespace XMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContextFactory<ApplicationDbContext>((sp, options) =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

                options.UseSqlServer(connectionString);
            });
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IDbContextFactoryProxy, DbContextFactoryProxy>();

            services.AddIntegrationServices(configuration);

            return services;
        }
    }
}
