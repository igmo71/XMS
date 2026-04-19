using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XMS.Application.Core.Common;

public abstract class BaseService
{
    protected readonly string _className;

    protected BaseService()
    {
        _className = GetType().Name;
    }

    protected Activity? StartActivity([CallerMemberName] string methodName = "")
    {
        var operationName = $"{_className}.{methodName}";

        var activity = AppTelemetry.ActivitySource.StartActivity(operationName, ActivityKind.Consumer)
            ?? new Activity(operationName).Start();

        return activity;
    }
}
