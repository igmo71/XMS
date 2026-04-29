using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using XMS.Application;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.EventBus;
using XMS.Modules;

namespace XMS.Infrastructure.Hosting;

public static class XmsHostApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddXmsHostDefaults(this WebApplicationBuilder builder, string serviceName)
    {
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
        });

        builder.Services.AddAppPersistenceInfrastructure(builder.Configuration);

        builder.Services.AddSingleton<IEventNamingService, EventNamingService>();
        builder.Services.AddRabbitMqEventConnectionFactory(builder.Configuration);
        builder.Services.AddIntegrationEventPublisher(builder.Configuration);

        builder.Services.AddAppEventBus();

        builder.Services.AddIntegrationServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationModules(builder.Configuration);

        builder.Services.AddAppOpenTelemetry(builder.Configuration, serviceName);

        return builder;
    }

    public static WebApplicationBuilder AddXmsIntegrationConsumers(this WebApplicationBuilder builder)
    {
        var assembliesWithHandlers = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => a.FullName?.StartsWith("XMS.", StringComparison.Ordinal) == true)
            .ToArray();

        var integrationEventHandlers = builder.Services.AddIntegrationEventHandlers(assembliesWithHandlers);
        builder.Services.AddIntegrationEventConsumer(builder.Configuration, integrationEventHandlers);

        return builder;
    }
}
