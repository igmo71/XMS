namespace XMS.Web.Components.Layout.Sections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppSections(this IServiceCollection services)
        {
            services.AddScoped<IAppSection, SectionConfig>();
            services.AddScoped<IAppSection, SectionAdmin>();
            services.AddScoped<IAppSection, SectionYuNu>();

            return services;
        }
    }
}
