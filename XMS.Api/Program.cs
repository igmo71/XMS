using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using XMS.Api.Endpoints;
using XMS.Application;
using XMS.Application.Common;
using XMS.Infrastructure;
using XMS.Infrastructure.Integration;

namespace XMS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);
            });

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource.AddService(AppTelemetry.ServiceName);
                    resource.AddAttributes(new Dictionary<string, object> { ["Application"] = "XMS" });
                })
                .WithTracing(tracing => tracing
                    .SetSampler(new AppTraceSampler())
                    .AddSource(AppTelemetry.SourceName)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation()
                    //.AddEntityFrameworkCoreInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        //options.Endpoint = new Uri("http://vm-igmo-dev:5341/ingest/otlp/v1/traces");
                        var endpoint = new Uri($"{builder.Configuration["Serilog:WriteTo:1:Args:serverUrl"]}/ingest/otlp/v1/traces");
                        options.Endpoint = (endpoint);
                        options.Protocol = OtlpExportProtocol.HttpProtobuf;
                    }));

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationServices();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapYuNuEndpoints();
            app.MapGodooEndpoints();

            app.Run();
        }
    }
}
