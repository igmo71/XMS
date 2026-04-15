using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Features.Catalog_КонтактныеЛицаПартнеров_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_Контрагенты_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature;
using XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;
using XMS.Integration.OneC.Ut.Features.Document_РасходныйКассовыйОрдер_Feature;
using XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;
using XMS.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;

namespace XMS.Integration.OneC.Ut;

public static class DependencyInjection
{
    public static IServiceCollection AddUtCServices(this IServiceCollection services)
    {
        services.AddHostedService<Catalog_КонтактныеЛицаПартнеров_NotificationConsumer>();
        services.AddScoped<ICatalog_КонтактныеЛицаПартнеров_NotificationHandler, Catalog_КонтактныеЛицаПартнеров_NotificationHandler>();
        services.AddScoped<ICatalog_КонтактныеЛицаПартнеров_Service, Catalog_КонтактныеЛицаПартнеров_Service>();

        services.AddHostedService<Catalog_Контрагенты_NotificationConsumer>();
        services.AddScoped<ICatalog_Контрагенты_NotificationHandler, Catalog_Контрагенты_NotificationHandler>();
        services.AddScoped<ICatalog_Контрагенты_Service, Catalog_Контрагенты_Service>();

        services.AddHostedService<Catalog_КСЗ_КатегорииЗатрат_NotificationConsumer>();
        services.AddScoped<ICatalog_КСЗ_КатегорииЗатрат_NotificationHandler, Catalog_КСЗ_КатегорииЗатрат_NotificationHandler>();
        services.AddScoped<ICatalog_КСЗ_КатегорииЗатрат_Service, Catalog_КСЗ_КатегорииЗатрат_Service>();

        services.AddHostedService<Catalog_Номенклатура_NotificationConsumer>();
        services.AddScoped<ICatalog_Номенклатура_NotificationHandler, Catalog_Номенклатура_NotificationHandler>();
        services.AddScoped<ICatalog_Номенклатура_Service, Catalog_Номенклатура_Service>();

        services.AddHostedService<Catalog_Партнеры_NotificationConsumer>();
        services.AddScoped<ICatalog_Партнеры_NotificationHandler, Catalog_Партнеры_NotificationHandler>();
        services.AddScoped<ICatalog_Партнеры_Service, Catalog_Партнеры_Service>();

        services.AddHostedService<Catalog_Пользователи_NotificationConsumer>();
        services.AddScoped<ICatalog_Пользователи_NotificationHandler, Catalog_Пользователи_NotificationHandler>();
        services.AddScoped<ICatalog_Пользователи_Service, Catalog_Пользователи_Service>();

        services.AddHostedService<Catalog_СтатьиДвиженияДенежныхСредств_NotificationConsumer>();
        services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_NotificationHandler, Catalog_СтатьиДвиженияДенежныхСредств_NotificationHandler>();
        services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_Service, Catalog_СтатьиДвиженияДенежныхСредств_Service>();

        services.AddHostedService<Document_ЗаказКлиента_NotificationConsumer>();
        services.AddScoped<IDocument_ЗаказКлиента_NotificationHandler, Document_ЗаказКлиента_NotificationHandler>();
        services.AddScoped<IDocument_ЗаказКлиента_Service, Document_ЗаказКлиента_Service>();

        services.AddHostedService<Document_РасходныйКассовыйОрдер_NotificationConsumer>();
        services.AddScoped<IDocument_РасходныйКассовыйОрдер_NotificationHandler, Document_РасходныйКассовыйОрдер_NotificationHandler>();
        services.AddScoped<IDocument_РасходныйКассовыйОрдер_Service, Document_РасходныйКассовыйОрдер_Service>();

        services.AddHostedService<Document_РеализацияТоваровУслуг_NotificationConsumer>();
        services.AddScoped<IDocument_РеализацияТоваровУслуг_NotificationHandler, Document_РеализацияТоваровУслуг_NotificationHandler>();
        services.AddScoped<IDocument_РеализацияТоваровУслуг_Service, Document_РеализацияТоваровУслуг_Service>();

        services.AddHostedService<Document_СписаниеБезналичныхДенежныхСредств_NotificationConsumer>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_NotificationHandler, Document_СписаниеБезналичныхДенежныхСредств_NotificationHandler>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_Service, Document_СписаниеБезналичныхДенежныхСредств_Service>();

        return services;
    }

    public static IEndpointRouteBuilder MapUtEndpoints(this IEndpointRouteBuilder builder)
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
}
