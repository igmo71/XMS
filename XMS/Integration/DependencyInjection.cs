using XMS.Integration.AD;
using XMS.Integration.Bitrix;
using XMS.Integration.OneS;

namespace XMS.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOneSServices(configuration);
            services.AddAdServices(configuration);
            services.AddBitrixServices(configuration);

            return services;
        }
    }
}
