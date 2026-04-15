using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XMS.Core.Abstractions.EventBus;

namespace XMS.Integration.OneC.Common;

internal static class IntegrationExtensions
{
    public static void AddIntegrationHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlers = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)));

        foreach (var handler in handlers)
        {
            var interfaceType = handler.GetInterfaces().First(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>));

            // Регистрируем в DI
            services.AddScoped(interfaceType, handler);

            // Регистрируем в нашем реестре типов для диспетчера
            var eventType = interfaceType.GetGenericArguments()[0];
            // Используем имя класса как ключ
            _registry.Register(eventType.Name, eventType);
        }
    }
}
