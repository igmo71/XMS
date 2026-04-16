using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RabbitMQ.Client;
using XMS.Core.Abstractions.Data;
using XMS.Core.Abstractions.EventBus;
using XMS.Core.Common;
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


    public static IServiceCollection AddAppEventBus(this IServiceCollection services, IConfiguration configuration, List<Type> handlerInterfaces)
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

        services.AddHostedService(sp => new RabbitMqConsumer(
            serviceProvider: sp,
            connectionFactory: sp.GetRequiredService<IConnectionFactory>(),
            eventNaming: sp.GetRequiredService<IEventNamingService>(),
            logger: sp.GetRequiredService<ILogger<RabbitMqConsumer>>(),
            handlers: handlerInterfaces));

        return services;
    }

    public static IServiceCollection AddAppOpenTelemetry(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {

        services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(serviceName);
                resource.AddAttributes(new Dictionary<string, object> { ["Application"] = "XMS" });
            })
            .WithTracing(tracing => tracing
                .AddSource(AppTelemetry.SourceName)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation()
                //.AddEntityFrameworkCoreInstrumentation()
                //.SetSampler(new AlwaysOnSampler())
                .SetSampler(new AppTraceSampler())
                //.AddConsoleExporter()
                .AddOtlpExporter(options =>
                {
                    // http://vm-xms-dev:5341/ingest/otlp/v1/traces
                    var seqServerUri = ResolveSeqServerUri(configuration);
                    options.Endpoint = new Uri(seqServerUri, "/ingest/otlp/v1/traces");

                    options.Protocol = OtlpExportProtocol.HttpProtobuf;
                }));

        return services;
    }

    private static Uri ResolveSeqServerUri(IConfiguration configuration)
    {
        var writeToSections = configuration.GetSection("Serilog:WriteTo").GetChildren();

        var seqSink = writeToSections.FirstOrDefault(e => e["Name"] == "Seq")
            ?? throw new InvalidOperationException("Seq Sink is not configured or invalid");

        var serverUrl = seqSink["Args:serverUrl"];

        if (!string.IsNullOrWhiteSpace(serverUrl) && Uri.TryCreate(serverUrl, UriKind.Absolute, out var serverUri))
            return serverUri;

        throw new InvalidOperationException("Seq ServerUri is not configured or invalid");
    }
}
