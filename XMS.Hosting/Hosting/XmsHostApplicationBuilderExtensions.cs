using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using XMS.Application;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Common;
using XMS.Application.EventBus;
using XMS.Hosting.Observability;
using XMS.Infrastructure;
using XMS.Modules;

namespace XMS.Hosting;

public static class XmsHostApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddXmsHostDefaults(this WebApplicationBuilder builder, string serviceName)
    {
        builder
            .AddXmsLogging()
            .AddXmsObservability(serviceName)
            .AddXmsPersistence()
            .AddXmsMessaging()
            .AddXmsApplicationServices()
            .AddXmsModules();

        return builder;
    }

    public static WebApplicationBuilder AddXmsLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
        });

        return builder;
    }

    public static WebApplicationBuilder AddXmsObservability(this WebApplicationBuilder builder, string serviceName)
    {
        builder.Services.AddOpenTelemetry()
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
                .AddRabbitMQInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                //.SetSampler(new AlwaysOnSampler())
                .SetSampler(new AppTraceSampler())
                //.AddConsoleExporter()
                .AddOtlpExporter(options =>
                {
                    // http://vm-xms-dev:5341/ingest/otlp/v1/traces
                    var seqServerUri = ResolveSeqServerUri(builder.Configuration);
                    options.Endpoint = new Uri(seqServerUri, "/ingest/otlp/v1/traces");

                    options.Protocol = OtlpExportProtocol.HttpProtobuf;
                }));

        return builder;
    }

    public static WebApplicationBuilder AddXmsPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddAppPersistenceInfrastructure(builder.Configuration);

        return builder;
    }

    public static WebApplicationBuilder AddXmsMessaging(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IEventNamingService, EventNamingService>();
        builder.Services.AddRabbitMqEventConnectionFactory(builder.Configuration);
        builder.Services.AddIntegrationEventPublisher(builder.Configuration);

        builder.Services.AddAppEventBus();

        return builder;
    }

    public static WebApplicationBuilder AddXmsApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddIntegrationServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        return builder;
    }

    public static WebApplicationBuilder AddXmsModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationModules(builder.Configuration);

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
