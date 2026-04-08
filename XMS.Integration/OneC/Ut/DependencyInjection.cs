using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;
using XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut;

public static class DependencyInjection
{
    public static IServiceCollection AddUtCServices(this IServiceCollection services)
    {
        services.AddHostedService<Catalog_КонтактныеЛицаПартнеров_EventConsumer>();
        services.AddScoped<ICatalog_КонтактныеЛицаПартнеров_EventHandler, Catalog_КонтактныеЛицаПартнеров_EventHandler>();
        services.AddScoped<ICatalog_КонтактныеЛицаПартнеров_Service, Catalog_КонтактныеЛицаПартнеров_Service>();

        services.AddHostedService<Catalog_Контрагенты_EventConsumer>();
        services.AddScoped<ICatalog_Контрагенты_EventHandler, Catalog_Контрагенты_EventHandler>();
        services.AddScoped<ICatalog_Контрагенты_Service, Catalog_Контрагенты_Service>();

        services.AddHostedService<Catalog_Номенклатура_EventConsumer>();
        services.AddScoped<ICatalog_Номенклатура_EventHandler, Catalog_Номенклатура_EventHandler>();
        services.AddScoped<ICatalog_Номенклатура_Service, Catalog_Номенклатура_Service>();

        services.AddHostedService<Catalog_Партнеры_EventConsumer>();
        services.AddScoped<ICatalog_Партнеры_EventHandler, Catalog_Партнеры_EventHandler>();
        services.AddScoped<ICatalog_Партнеры_Service, Catalog_Партнеры_Service>();

        services.AddHostedService<Catalog_СтатьиДвиженияДенежныхСредств_EventConsumer>();
        services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_EventHandler, Catalog_СтатьиДвиженияДенежныхСредств_EventHandler>();
        services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_Service, Catalog_СтатьиДвиженияДенежныхСредств_Service>();

        services.AddHostedService<Document_ЗаказКлиента_EventConsumer>();
        services.AddScoped<IDocument_ЗаказКлиента_EventHandler, Document_ЗаказКлиента_EventHandler>();
        services.AddScoped<IDocument_ЗаказКлиента_Service, Document_ЗаказКлиента_Service>();

        services.AddHostedService<Document_РеализацияТоваровУслуг_EventConsumer>();
        services.AddScoped<IDocument_РеализацияТоваровУслуг_EventHandler, Document_РеализацияТоваровУслуг_EventHandler>();
        services.AddScoped<IDocument_РеализацияТоваровУслуг_Service, Document_РеализацияТоваровУслуг_Service>();

        services.AddHostedService<Document_СписаниеБезналичныхДенежныхСредств_EventConsumer>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler, Document_СписаниеБезналичныхДенежныхСредств_EventHandler>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_Service, Document_СписаниеБезналичныхДенежныхСредств_Service>();

        return services;
    }

    public static IEndpointRouteBuilder MapUtEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.Map_Catalog_КонтактныеЛицаПартнеров_Endpoints();
        builder.Map_Catalog_Контрагенты_Endpoints();
        builder.Map_Catalog_Номенклатура_Endpoints();
        builder.MapCatalog_Партнеры_Endpoints();
        builder.MapCatalog_СтатьиДвиженияДенежныхСредств_Endpoints();
        builder.MapDocument_ЗаказКлиента_Endpoints();
        builder.MapDocument_РеализацияТоваровУслуг_Endpoints();
        builder.MapDocument_СписаниеБезналичныхДенежныхСредств_Endpoints();

        return builder;
    }
}
