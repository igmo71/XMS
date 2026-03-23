using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Buh;
using XMS.Integration.OneC.Ut;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Integration.OneC.Zup;

namespace XMS.Integration.OneC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOneCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOneCClient<UtClient, UtClientConfig>(configuration);
            services.AddOneCClient<BuhClient, BuhClientConfig>(configuration);
            services.AddOneCClient<ZupClient, ZupClientConfig>(configuration);

            services.AddScoped<IOneCUtService, UtService>();
            services.AddScoped<IOneCBuhService, BuhService>();
            services.AddScoped<IOneCZupService, ZupService>();

            services.AddScoped<ICatalog_Партнеры_Service, Catalog_Партнеры_Service>();
            services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_Service, Catalog_СтатьиДвиженияДенежныхСредств_Service>();
            services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_Service, Document_СписаниеБезналичныхДенежныхСредств_Service>();

            return services;
        }

        public static IEndpointRouteBuilder MapOneCEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints();

            return builder;
        }

        private static IServiceCollection AddOneCClient<TClient, TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TClient : class
            where TConfig : OneCClientConfig
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
