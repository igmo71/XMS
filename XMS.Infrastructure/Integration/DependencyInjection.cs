using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Infrastructure.Integration.AD;
using XMS.Infrastructure.Integration.Bitrix;
using XMS.Infrastructure.Integration.OneS;

namespace XMS.Infrastructure.Integration
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
