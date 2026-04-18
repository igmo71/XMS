using Microsoft.Extensions.DependencyInjection;
using XMS.Core.Abstractions.EventBus;
using XMS.EventBus.Abstractions;

namespace XMS.EventBus;

public static class DependencyInjection
{
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
}
