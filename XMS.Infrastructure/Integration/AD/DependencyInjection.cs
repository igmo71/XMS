using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Application.Abstractions.Integration;
using XMS.Infrastructure.Integration.AD.Application;
using XMS.Infrastructure.Integration.AD.Infrastructure;

namespace XMS.Infrastructure.Integration.AD
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(AdClientConfig));

            services.Configure<AdClientConfig>(configSection);

            var config = configSection.Get<AdClientConfig>()
                ?? throw new InvalidOperationException("AdClientConfig not found");

            services.AddHttpClient<AdClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
            });

            services.AddScoped<IAdService, AdService>();

            return services;
        }
    }
}
