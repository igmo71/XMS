namespace XMS.Web
{
    public static class AppSettings
    {
        public static class MaxLength
        {
            public const int GUID = 36;
            public const int NAME = 150;
            public const int SID = 45;
        }

        public static class AuthSchemes
        {
            public const string Basic = "Basic";
            public const string Bearer = "Bearer";
        }
    }
}
