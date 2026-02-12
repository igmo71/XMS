using System.Diagnostics;

namespace XMS.Web.Core
{
    public class AppTelemetry
    {
        public const string ServiceName = "XMS.Service";

        public const string SourceName = "XMS.Source";

        public static readonly ActivitySource ActivitySource = new(SourceName);
    }
}
