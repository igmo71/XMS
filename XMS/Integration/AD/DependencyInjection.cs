using XMS.Integration.AD.Application;
using XMS.Integration.AD.Infrastructure;

namespace XMS.Integration.AD
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdSServices(this IServiceCollection services, IConfiguration configuration)
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
