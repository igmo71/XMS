using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Application.Common.Integration;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Application;
using XMS.Modules.CostModule.Infrastructure.OneS;

namespace XMS.Modules.CostModule
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCostModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOneSClient<CostUtClient, UtClientConfig>(configuration);
            services.AddScoped<ICostUtService, CostUtService>();

            services.AddScoped<ICashFlowCostService, CashFlowCostService>();
            services.AddScoped<ICashFlowItemService, CashFlowItemService>();
            services.AddScoped<ICostCategoryItemService, CostCategoryItemService>();
            services.AddScoped<ICostCategoryService, CostCategoryService>();
            services.AddScoped<ICostItemService, CostItemService>();

            services.AddScoped<IDocument_СписаниеБезналичныхДенежныхСредств_Service, Document_СписаниеБезналичныхДенежныхСредств_Service>();

            return services;
        }
    }
}
