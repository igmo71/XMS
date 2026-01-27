using System.Net.Http.Headers;
using System.Text;
using XMS.Integration.OneS.Ut.Infrastructure;

namespace XMS.Integration.OneS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOneSServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuhClient(configuration);

            return services;
        }

        public static IServiceCollection AddBuhClient(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(nameof(BuhClientConfig));

            services.Configure<BuhClientConfig>(configurationSection);

            var config = configurationSection.Get<BuhClientConfig>()
                ?? throw new InvalidOperationException("BuhConfig not found");

            services.AddHttpClient<BuhClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.UserName}:{config.Password}")));

            });

            return services;
        }
    }
}
