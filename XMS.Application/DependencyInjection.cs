using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Abstractions.Integration.OneC.Events;
using XMS.Application.Abstractions.Integration.OneC.Ut;
using XMS.Application.Abstractions.Services;
using XMS.Application.Endpoints;
using XMS.Application.EventBus;
using XMS.Application.Integration.AD;
using XMS.Application.Integration.Bitrix;
using XMS.Application.Integration.OneC.Buh.Api;
using XMS.Application.Integration.OneC.Buh.ODataClient;
using XMS.Application.Integration.OneC.Common;
using XMS.Application.Integration.OneC.Ut.Api;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Application.Integration.OneC.Ut.ODataClient;
using XMS.Application.Integration.OneC.Zup.Api;
using XMS.Application.Integration.OneC.Zup.ODataClient;
using XMS.Application.Services;

namespace XMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeBuhService, EmployeeBuhService>();
        services.AddScoped<IEmployeeZupService, EmployeeZupService>();
        services.AddScoped<IJobTitleService, JobTitleService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IUserAdService, UserAdService>();
        services.AddScoped<IUserUtService, UserUtService>();

        return services;
    }

    public static IServiceCollection AddAppEventHandlers(this IServiceCollection services)
    {
        var handlers = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IAppEventHandler<>)));

        foreach (var handler in handlers)
        {
            var interfaceType = handler.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAppEventHandler<>));

            services.AddScoped(interfaceType, handler);
        }

        return services;
    }

    public static IServiceCollection AddAppEventBus(this IServiceCollection services)
    {
        services.AddSingleton<AppEventChannel>();
        services.AddHostedService(sp => sp.GetRequiredService<AppEventChannel>());
        services.AddSingleton<IAppEventPublisher, AppEventPublisher>();
        services.AddAppEventHandlers();

        return services;
    }

    public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOneCServices(configuration);
        services.AddAdServices(configuration);
        services.AddBitrixServices(configuration);

        return services;
    }

    public static List<Type> AddIntegrationEventHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            return [];
        }

        var handlerMappings = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>))
                .Select(i => new { Interface = i, Implementation = t }))
            .ToList();

        foreach (var map in handlerMappings)
        {
            services.AddScoped(map.Interface, map.Implementation);
        }

        return handlerMappings
            .Select(m => m.Interface)
            .Distinct()
            .ToList();
    }

    public static IEndpointRouteBuilder MapApplicationEndpints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapTestBodyEndpint();
        routeBuilder.MapEmployeeEndpints();

        return routeBuilder;
    }

    public static IEndpointRouteBuilder MapIntegrationEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapOneCEndpoints();

        return builder;
    }

    private static IServiceCollection AddAdServices(this IServiceCollection services, IConfiguration configuration)
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

    private static IServiceCollection AddBitrixServices(this IServiceCollection services, IConfiguration configuration)
    {
        var configSection = configuration.GetSection(nameof(BitrixClientConfig));

        services.Configure<BitrixClientConfig>(configSection);

        var config = configSection.Get<BitrixClientConfig>()
            ?? throw new InvalidOperationException("BitrixClientConfig not found");

        services.AddHttpClient<BitrixClient>(client =>
        {
            client.BaseAddress = new Uri(config.BaseAddress);
        });

        services.AddScoped<IBitrixService, BitrixService>();

        return services;
    }

    private static IServiceCollection AddOneCServices(this IServiceCollection services, IConfiguration configuration)
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

    private static IServiceCollection AddUtCServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalog_КонтактныеЛицаПартнеров_Service, Catalog_КонтактныеЛицаПартнеров_Service>();
        services.AddScoped<ICatalog_Контрагенты_Service, Catalog_Контрагенты_Service>();
        services.AddScoped<ICatalog_Номенклатура_Service, Catalog_Номенклатура_Service>();
        services.AddScoped<ICatalog_Партнеры_Service, Catalog_Партнеры_Service>();
        services.AddScoped<ICatalog_Пользователи_Service, Catalog_Пользователи_Service>();
        services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_Service, Catalog_СтатьиДвиженияДенежныхСредств_Service>();
        services.AddScoped<IDocument_ЗаказКлиента_Service, Document_ЗаказКлиента_Service>();
        services.AddScoped<IDocument_РасходныйКассовыйОрдер_Service, Document_РасходныйКассовыйОрдер_Service>();
        services.AddScoped<IDocument_РеализацияТоваровУслуг_Service, Document_РеализацияТоваровУслуг_Service>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_Service, Document_СписаниеБезналичныхДенежныхСредств_Service>();

        return services;
    }

    private static IEndpointRouteBuilder MapOneCEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapUtEndpoints();

        return builder;
    }

    private static IEndpointRouteBuilder MapUtEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapCatalog_КонтактныеЛицаПартнеров_Endpoints();
        builder.MapCatalog_Контрагенты_Endpoints();
        builder.MapCatalog_Номенклатура_Endpoints();
        builder.MapCatalog_Партнеры_Endpoints();
        builder.MapCatalog_Пользователи_Endpoints();
        builder.MapCatalog_СтатьиДвиженияДенежныхСредств_Endpoints();
        builder.MapDocument_ЗаказКлиента_Endpoints();
        builder.MapDocument_РасходныйКассовыйОрдер_Endpoints();
        builder.MapDocument_РеализацияТоваровУслуг_Endpoints();
        builder.MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints();

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
