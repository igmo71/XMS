using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XMS.Modules.GodooModule.Abstractions;
using XMS.Modules.GodooModule.Application;
using XMS.Modules.GodooModule.Infrastructure.OneS.Buh;
using XMS.Modules.GodooModule.Infrastructure.Yunu;

namespace XMS.Modules.GodooModule;

public static class DependencyInjection
{
    public static IServiceCollection AddGodoModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOneSServices(configuration);

        services.AddYunuServices(configuration);

        services.AddScoped<IGodooService, GodooService>();

        return services;
    }
}
