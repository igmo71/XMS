using XMS.Web.Integration.AD;
using XMS.Web.Integration.Bitrix;
using XMS.Web.Integration.OneS;

namespace XMS.Web.Integration
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
