using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration.OneC.Events;
using XMS.Infrastructure.Data;
using XMS.Infrastructure.EventBus;

namespace XMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAppPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContextFactory<ApplicationDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name]);
        });
        services.AddDatabaseDeveloperPageExceptionFilter();



        services.AddScoped<IDbContextFactoryProxy, DbContextFactoryProxy>();

        return services;
    }
    public static IServiceCollection AddRabbitMqEventConnectionFactory(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfig = configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>()
                    ?? throw new InvalidOperationException("RabbitMqConfig Not Found");

        services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
        {
            HostName = rabbitMqConfig.Host,
            UserName = rabbitMqConfig.Username,
            Password = rabbitMqConfig.Password
        });

        return services;
    }

    public static IServiceCollection AddIntegrationEventPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IIntegrationEventPublisher, RabbitMqIntegrationPublisher>();

        return services;
    }

    public static IServiceCollection AddIntegrationEventConsumer(this IServiceCollection services, IConfiguration configuration, List<Type> handlerInterfaces)
    {
        services.AddHostedService(sp => new RabbitMqIntegrationConsumer(
            serviceProvider: sp,
            connectionFactory: sp.GetRequiredService<IConnectionFactory>(),
            eventNaming: sp.GetRequiredService<IEventNamingService>(),
            logger: sp.GetRequiredService<ILogger<RabbitMqIntegrationConsumer>>(),
            handlers: handlerInterfaces));

        return services;
    }
}
