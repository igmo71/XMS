using Scalar.AspNetCore;
using XMS.Application;
using XMS.Infrastructure.Hosting;
using XMS.Modules;

namespace XMS.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Console.WriteLine($"XMS.Api Environment: {builder.Environment.EnvironmentName}");

        builder
            .AddXmsHostDefaults("XMS.Api")
            .AddXmsIntegrationConsumers();

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddProblemDetails();

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
