using System.Diagnostics;

namespace XMS.Application.Core.Common;

public class AppTelemetry
{
    public const string SourceName = "XMS.Source";

    public static readonly ActivitySource ActivitySource = new(SourceName);
}
