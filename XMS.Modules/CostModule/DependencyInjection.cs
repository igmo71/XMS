using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Abstractions.Integration;
using XMS.Modules.CostModule.Application;
using XMS.Modules.CostModule.Integration;

namespace XMS.Modules.CostModule;

public static class DependencyInjection
{
    public static IServiceCollection AddCostModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICostAllocationService, CostAllocationService>();
        services.AddScoped<ICostCatalogUtService, CostCatalogUtService>();
        services.AddScoped<ICostCategoryItemService, CostCategoryItemService>();
        services.AddScoped<ICostCategoryService, CostCategoryService>();
        services.AddScoped<ICostCategoryIntegrationService, CostCategoryIntegrationService>();
        services.AddScoped<ICostItemService, CostItemService>();

        // Integration

        services.AddHostedService<Document_РасходныйКассовыйОрдер_EventConsumer>();
        services.AddScoped<IDocument_РасходныйКассовыйОрдер_EventHandler, Document_РасходныйКассовыйОрдер_EventHandler>();

        services.AddHostedService<Document_СписаниеБезналичныхДенежныхСредств_EventConsumer>();
        services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_EventHandler, Document_СписаниеБезналичныхДенежныхСредств_EventHandler>();

        return services;
    }
}
