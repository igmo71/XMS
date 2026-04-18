using Scalar.AspNetCore;
using Serilog;
using XMS.Application;
using XMS.Core.Abstractions.EventBus;
using XMS.Core.Common;
using XMS.EventBus;
using XMS.EventBus.Abstractions;
using XMS.Infrastructure;
using XMS.Integration;
using XMS.Modules;

namespace XMS.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Console.WriteLine($"XMS.Api Environment: {builder.Environment.EnvironmentName}");

        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
        });

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddProblemDetails();

        builder.Services.AddAppPersistenceInfrastructure(builder.Configuration);

        builder.Services.AddSingleton<IEventNamingService, EventNamingService>();
        builder.Services.AddRabbitMqEventConnectionFactory(builder.Configuration);
        builder.Services.AddIntegrationEventPublisher(builder.Configuration);

        var assembliesWithHandlers = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => a.FullName!.StartsWith("XMS."))
            .ToArray();
        var integrationEventHandlers = builder.Services.AddAppIntegrationEventHandlers(assembliesWithHandlers);
        builder.Services.AddIntegrationEventConsumer(builder.Configuration, integrationEventHandlers);

        builder.Services.AddSingleton<IAppEventPublisher, AppEventPublisher>();
        builder.Services.AddAppEventHandlers();

        builder.Services.AddAppIntegrationServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationModules(builder.Configuration);

        builder.Services.AddAppOpenTelemetry(builder.Configuration, "XMS.Api");

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapApplicationEndpints();
        app.MapIntegrationEndpoints();

        app.MapModulesEndpoints();

        app.Run();
    }
}