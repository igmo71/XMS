using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Infrastructure.Integration.AD;
using XMS.Infrastructure.Integration.Bitrix;
using XMS.Infrastructure.Integration.OneS;
using XMS.Infrastructure.Integration.YuNu;

namespace XMS.Infrastructure.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOneSServices(configuration);
            services.AddAdServices(configuration);
            services.AddBitrixServices(configuration);
            services.AddYuNuServices(configuration);

            return services;
        }
    }
}
