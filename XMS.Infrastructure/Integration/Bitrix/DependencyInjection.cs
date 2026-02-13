using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Infrastructure.Integration.Bitrix.Application;
using XMS.Infrastructure.Integration.Bitrix.Infrastructure;

namespace XMS.Infrastructure.Integration.Bitrix
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBitrixServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(BitrixClientConfig));

            services.Configure<BitrixClientConfig>(configSection);

            var config = configSection.Get<BitrixClientConfig>()
                ?? throw new InvalidOperationException("BitrixClientConfig not found");

            services.AddHttpClient<BitrixClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
            });

            services.AddScoped<IBitrixService, BitrixService>();

            return services;
        }
    }
}
