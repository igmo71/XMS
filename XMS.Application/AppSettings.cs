namespace XMS.Application
{
    public static class AppSettings
    {
        public static class Default
        {
            public const int Skip = 0;
            public const int Take = 100;
            public const int MaxTake = 100;            
        }

        public static class OneS
        {
            public const int GUID = 36;
            public const int ID = 50;
            public const int VALUE = 100;
            public const int DESCRIPTION = 150;
            public const int FULL_NAME = 250;
            public const int RECORD_TYPE = 7;
            public const int COMMENT = 450;
        }

        public const int MaxSubordinatesCount = 1000;
    }
}
