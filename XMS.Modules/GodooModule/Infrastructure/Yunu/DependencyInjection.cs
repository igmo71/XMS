using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Modules.GodooModule.Abstractions;

namespace XMS.Modules.GodooModule.Infrastructure.Yunu
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddYunuServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(YunuClientConfig));

            services.Configure<YunuClientConfig>(configSection);

            var config = configSection.Get<YunuClientConfig>()
                ?? throw new InvalidOperationException("YunuClientConfig not found");

            services.AddHttpClient<YunuClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
            });

            services.AddScoped<IYunuService, YunuService>();

            return services;
        }
    }
}
