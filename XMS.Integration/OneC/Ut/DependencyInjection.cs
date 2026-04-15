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
        services.AddScoped<ICatalog_КонтактныеЛицаПартнеров_Service, Catalog_КонтактныеЛицаПартнеров_Service>();
        services.AddScoped<ICatalog_Контрагенты_Service, Catalog_Контрагенты_Service>();
        services.AddScoped<ICatalog_КСЗ_КатегорииЗатрат_Service, Catalog_КСЗ_КатегорииЗатрат_Service>();
        services.AddScoped<ICatalog_Номенклатура_Service, Catalog_Номенклатура_Service>();
        services.AddScoped<ICatalog_Партнеры_Service, Catalog_Партнеры_Service>();
        services.AddScoped<ICatalog_Пользователи_Service, Catalog_Пользователи_Service>();
        services.AddScoped<ICatalog_СтатьиДвиженияДенежныхСредств_Service, Catalog_СтатьиДвиженияДенежныхСредств_Service>();
        services.AddScoped<IDocument_ЗаказКлиента_Service, Document_ЗаказКлиента_Service>();
        services.AddScoped<IDocument_РасходныйКассовыйОрдер_Service, Document_РасходныйКассовыйОрдер_Service>();
        services.AddScoped<IDocument_РеализацияТоваровУслуг_Service, Document_РеализацияТоваровУслуг_Service>();
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
