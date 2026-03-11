using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using XMS.Application.Common.Integration;
using XMS.Infrastructure.Integration.OneS;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Application;
using XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Infrastructure;

namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOneSServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOneSClient<GodooBuhClient, GodooBuhClientConfig>(configuration);

            
            services.AddScoped<IGodooOneSBuhService, BuhService>();

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
