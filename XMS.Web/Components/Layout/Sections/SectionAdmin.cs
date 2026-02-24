using Microsoft.AspNetCore.Components;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionAdmin : IAppSection
    {
        public string Key => "admin";
        public string Title => "Админка";

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuAdmin));
            builder.CloseComponent();
        };
    }
}
