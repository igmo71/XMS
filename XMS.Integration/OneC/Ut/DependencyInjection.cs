using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut;

public static class DependencyInjection
{
    public static IServiceCollection AddUtCServices(this IServiceCollection services)
    {
        services.AddHostedService<Catalog_Партнеры_EventConsumer>();
        services.AddScoped<ICatalog_Партнеры_EventHandler, Catalog_Партнеры_EventHandler>();
        services.AddScoped<ICatalog_Партнеры_Service, Catalog_Партнеры_Service>();

        services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_Service, Catalog_СтатьиДвиженияДенежныхСредств_Service>();

        services.AddHostedService<Document_СписаниеБезналичныхДенежныхСредств_EventConsumer>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler, Document_СписаниеБезналичныхДенежныхСредств_EventHandler>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_Service, Document_СписаниеБезналичныхДенежныхСредств_Service>();

        return services;
    }

    public static IEndpointRouteBuilder MapUtEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapDocument_Catalog_Партнеры_Endpoints();
        builder.MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints();

        return builder;
    }
}
