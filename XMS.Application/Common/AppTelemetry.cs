using System.Diagnostics;

namespace XMS.Application.Common
{
    public class AppTelemetry
    {
        public const string ServiceName = "XMS.Service";

        public const string SourceName = "XMS.Source";

        public static readonly ActivitySource ActivitySource = new(SourceName);
    }
}
