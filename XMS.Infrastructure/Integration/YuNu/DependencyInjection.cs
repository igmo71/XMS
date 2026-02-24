using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Application.Abstractions.Integration;
using XMS.Infrastructure.Integration.YuNu.Application;
using XMS.Infrastructure.Integration.YuNu.Infrastructure;

namespace XMS.Infrastructure.Integration.YuNu
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddYuNuServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(YuNuClientConfig));

            services.Configure<YuNuClientConfig>(configSection);

            var config = configSection.Get<YuNuClientConfig>()
                ?? throw new InvalidOperationException("YuNuClientConfig not found");

            services.AddHttpClient<YuNuClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
            });

            services.AddScoped<IYuNuService, YuNuService>();

            return services;
        }
    }
}
