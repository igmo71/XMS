using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using MudBlazor.Translations;
using Serilog;
using System.Globalization;
using XMS.Application.Abstractions.Services;
using XMS.Application.Services;
using XMS.Domain.Models;
using XMS.Hosting;
using XMS.Infrastructure.Data;
using XMS.Web.Components;
using XMS.Web.Components.Account;
using XMS.Web.Components.Layout;
using XMS.Web.Components.Layout.Sections;

namespace XMS.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var culture = new CultureInfo("ru-RU");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        var builder = WebApplication.CreateBuilder(args);
        Console.WriteLine($"XMS.Web Environment: {builder.Environment.EnvironmentName}");

        builder.AddXmsHostDefaults("XMS.Web");

        // Add services to the container.

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddMudServices();
        builder.Services.AddMudTranslations();

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

        builder.Services.AddScoped<ThemeState>();

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
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.User.RequireUniqueEmail = true;
            //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        });

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        builder.Services.AddScoped<IWebUserAccessor, WebUserAccessor>();

        builder.Services.AddAppSections();

        var app = builder.Build();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

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

        app.MapPost("/ui/theme/{theme}", (string theme, HttpContext httpContext) =>
        {
            if (theme is not AppSettings.Theme.Light and not AppSettings.Theme.Dark)
                return Results.BadRequest();

            httpContext.Response.Cookies.Append(
                AppSettings.Theme.CookieName,
                theme,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax,
                    Secure = httpContext.Request.IsHttps
                });

            return Results.Ok();
        });

        app.Run();
    }
}
