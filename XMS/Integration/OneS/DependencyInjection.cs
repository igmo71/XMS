using System.Net.Http.Headers;
using System.Net.Mime;
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
            services.AddOneSClient<UtClient, UtClientConfig>(configuration);
            services.AddOneSClient<BuhClient, BuhClientConfig>(configuration);
            services.AddOneSClient<ZupClient, ZupClientConfig>(configuration);

            services.AddScoped<IUtService, UtService>();
            services.AddScoped<IBuhService, BuhService>();
            services.AddScoped<IZupService, ZupService>();

            return services;
        }

        private static IServiceCollection AddOneSClient<TClient, TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TClient : class
            where TConfig : OneSClientConfig
        {
            var sectionName = typeof(TConfig).Name;
            var configSection = configuration.GetSection(sectionName);

            services.Configure<TConfig>(configSection);

            var config = configSection.Get<TConfig>()
                ?? throw new InvalidOperationException($"{sectionName} not found in configuration");

            services.AddHttpClient<TClient>(client =>
            {
                client.BaseAddress = new Uri(config.BaseAddress);

                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.UserName}:{config.Password}"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            return services;
        }
    }
}
