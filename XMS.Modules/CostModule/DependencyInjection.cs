using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Application;

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

        services.AddHostedService<Document_РасходныйКассовыйОрдер_ReceivedEventConsumer>();
        services.AddHostedService<Document_РасходныйКассовыйОрдер_DeletedEventConsumer>();

        return services;
    }
}
