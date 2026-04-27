namespace XMS.Web.Components.Layout;

public sealed class ThemeState
{
    public bool IsDarkMode { get; private set; }

    public void SetTheme(string? theme)
    {
        IsDarkMode = string.Equals(theme, AppSettings.Theme.Dark, StringComparison.OrdinalIgnoreCase);
    }

    public string CurrentTheme => IsDarkMode ? AppSettings.Theme.Dark : AppSettings.Theme.Light;
}
