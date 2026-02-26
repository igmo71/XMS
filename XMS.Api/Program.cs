using Serilog;
using XMS.Api.Endpoints;
using XMS.Application;
using XMS.Infrastructure;

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
