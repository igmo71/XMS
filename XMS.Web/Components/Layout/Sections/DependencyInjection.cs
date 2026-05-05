namespace XMS.Web.Components.Layout.Sections;

public static class DependencyInjection
{
    public static IServiceCollection AddAppSections(this IServiceCollection services)
    {
        services.AddScoped<IAppSection, SectionCost>();
        //services.AddScoped<IAppSection, SectionWarehouse>();
        services.AddScoped<IAppSection, SectionConfig>();
        services.AddScoped<IAppSection, SectionAdmin>();
        services.AddScoped<IAppSection, SectionApi>();

        return services;
    }
}
