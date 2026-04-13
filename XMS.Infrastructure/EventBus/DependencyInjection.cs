using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using XMS.Core.Abstractions.EventBus;

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

        services.AddSingleton<IEventPublisher, RabbitPublisher>();

        return services;
    }
}
