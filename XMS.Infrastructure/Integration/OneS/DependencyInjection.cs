using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using XMS.Application.Abstractions.Integration;
using XMS.Infrastructure.Integration.OneS.Buh.Application;
using XMS.Infrastructure.Integration.OneS.Buh.Infrastructure;
using XMS.Infrastructure.Integration.OneS.Ut.Application;
using XMS.Infrastructure.Integration.OneS.Ut.Infrastructure;
using XMS.Infrastructure.Integration.OneS.Zup.Application;
using XMS.Infrastructure.Integration.OneS.Zup.Infrastructure;

namespace XMS.Infrastructure.Integration.OneS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOneSServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOneSClient<UtClient, UtClientConfig>(configuration);
            services.AddOneSClient<BuhClient, BuhClientConfig>(configuration);
            services.AddOneSClient<ZupClient, ZupClientConfig>(configuration);

            services.AddScoped<IOneSUtService, UtService>();
            services.AddScoped<IOneSBuhService, BuhService>();
            services.AddScoped<IOneSZupService, ZupService>();

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
