using XMS.Integration.AD;
using XMS.Integration.OneS;

namespace XMS.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegrationSServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOneSServices(configuration);
            services.AddAdServices(configuration);

            return services;
        }
    }
}
