using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using XMS.Application.Abstractions;
using XMS.Application.Common;
using XMS.Infrastructure.Data;
using XMS.Infrastructure.Integration;

namespace XMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenTelemetry()
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
                        var endpoint = new Uri($"{configuration["Serilog:WriteTo:1:Args:serverUrl"]}/ingest/otlp/v1/traces");
                        options.Endpoint = (endpoint);
                        options.Protocol = OtlpExportProtocol.HttpProtobuf;
                    }));


            services.AddDbContextFactory<ApplicationDbContext>((sp, options) =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

                options.UseSqlServer(connectionString);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IDbContextFactoryProxy, DbContextFactoryProxy>();

            services.AddIntegrationServices(configuration);

            return services;
        }
    }
}
