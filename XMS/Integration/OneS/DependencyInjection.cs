using System.Net.Http.Headers;
using System.Text;
using XMS.Integration.OneS.Abstractions;
using XMS.Integration.OneS.Buh.Application;
using XMS.Integration.OneS.Buh.Infrastructure;
using XMS.Integration.OneS.Ut.Application;
using XMS.Integration.OneS.Ut.Infrastructure;
using XMS.Integration.OneS.Zup.Application;
using XMS.Integration.OneS.Zup.Infrastructure;

namespace XMS.Integration.OneS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOneSServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuhClient(configuration);
            services.AddSingleton<IBuhService, BuhService>();

            services.AddZupClient(configuration);
            services.AddSingleton<IZupService, ZupService>();

            services.AddUtClient(configuration);
            services.AddSingleton<IUtService, UtService>();

            return services;
        }

        public static IServiceCollection AddBuhClient(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(nameof(BuhClientConfig));

            services.Configure<BuhClientConfig>(configurationSection);

            var config = configurationSection.Get<BuhClientConfig>()
                ?? throw new InvalidOperationException("BuhClientConfig not found");

            services.AddHttpClient<BuhClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.UserName}:{config.Password}")));

            });

            return services;
        }

        public static IServiceCollection AddZupClient(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(ZupClientConfig));

            services.Configure<ZupClientConfig>(configSection);

            var config = configSection.Get<ZupClientConfig>()
                ?? throw new InvalidOperationException("ZupClientConfig not found");

            services.AddHttpClient<ZupClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.UserName}:{config.Password}")));

            });

            return services;
        }

        public static IServiceCollection AddUtClient(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(nameof(UtClientConfig));

            services.Configure<UtClientConfig>(configurationSection);

            var config = configurationSection.Get<UtClientConfig>()
                ?? throw new InvalidOperationException("UtClientConfig not found");

            services.AddHttpClient<UtClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.UserName}:{config.Password}")));

            });

            return services;
        }
    }
}
