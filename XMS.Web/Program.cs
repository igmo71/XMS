using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using MudBlazor.Translations;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Globalization;
using XMS.Application;
using XMS.Application.Common;
using XMS.Domain.Models;
using XMS.Infrastructure;
using XMS.Infrastructure.Data;
using XMS.Web.Components;
using XMS.Web.Components.Account;

namespace XMS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var culture = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

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
            builder.Services.AddMudServices();
            builder.Services.AddMudTranslations();

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;

            });

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            builder.Services.AddApplicationServices();

            builder.Services.AddScoped<AuthService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.UseSerilogRequestLogging();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
