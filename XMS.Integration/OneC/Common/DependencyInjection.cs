using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using XMS.Integration.OneC.Api;
using XMS.Integration.OneC.Buh.Api;
using XMS.Integration.OneC.Buh.ODataClient;
using XMS.Integration.OneC.Ut;
using XMS.Integration.OneC.Ut.Api;
using XMS.Integration.OneC.Ut.ODataClient;
using XMS.Integration.OneC.Zup.Api;
using XMS.Integration.OneC.Zup.ODataClient;

namespace XMS.Integration.OneC.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddOneCServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOneCUtService, UtService>();
        services.AddScoped<IOneCBuhService, BuhService>();
        services.AddScoped<IOneCZupService, ZupService>();

        services.AddOneCClient<UtClient, UtClientConfig>(configuration);
        services.AddOneCClient<BuhClient, BuhClientConfig>(configuration);
        services.AddOneCClient<ZupClient, ZupClientConfig>(configuration);

        services.AddUtCServices();

        return services;
    }

    public static IEndpointRouteBuilder MapOneCEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapUtEndpoints();

        return builder;
    }

    private static IServiceCollection AddOneCClient<TClient, TConfig>(this IServiceCollection services, IConfiguration configuration)
        where TClient : class
        where TConfig : ODataClientConfig
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
