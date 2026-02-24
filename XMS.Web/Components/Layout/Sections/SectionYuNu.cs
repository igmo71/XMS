using Microsoft.AspNetCore.Components;

namespace XMS.Web.Components.Layout.Sections
{
    public class SectionYuNu : IAppSection
    {
        public string Key => "yunu";
        public string Title => "YuNu";

        public RenderFragment Menu => builder =>
        {
            builder.OpenComponent(0, componentType: typeof(NavMenuYuNu));
            builder.CloseComponent();
        };
    }
}
