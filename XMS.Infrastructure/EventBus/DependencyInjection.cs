using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Reflection;
using XMS.Core.Abstractions.EventBus;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Infrastructure.EventBus;

public static class DependencyInjection
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfig = configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>()
                    ?? throw new InvalidOperationException("RabbitMqConfig Not Found");

        services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
        {
            HostName = rabbitMqConfig.Host,
            UserName = rabbitMqConfig.Username,
            Password = rabbitMqConfig.Password
        });

        services.AddSingleton<IEventPublisher, RabbitMqPublisher>();

        var handlers = Assembly.GetAssembly(typeof(IIntegrationEventHandler<>))?
            .GetTypes()
            .SelectMany(t => t.GetInterfaces())
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>))
            .ToList() ??
            throw new InvalidOperationException("No integration event handlers found");

        services.AddHostedService(sp => new RabbitMqConsumer(
            serviceProvider: sp,
            connectionFactory: sp.GetRequiredService<IConnectionFactory>(),
            hostEnvironment: sp.GetRequiredService<IHostEnvironment>(),
            logger: sp.GetRequiredService<ILogger<RabbitMqConsumer>>(),
            handlers: handlers
        ));

        return services;
    }
}
