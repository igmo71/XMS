namespace XMS.Web;

public static class AppSettings
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }

    public static class AuthSchemes
    {
        public const string Basic = "Basic";
        public const string Bearer = "Bearer";
    }
    public static class Theme
    {
        public const string CookieName = "xms-theme";
        public const string Light = "light";
        public const string Dark = "dark";
    }
}
