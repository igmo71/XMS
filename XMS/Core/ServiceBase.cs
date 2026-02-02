using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XMS.Core
{
    public abstract class ServiceBase
    {
        protected readonly string _className;

        protected ServiceBase()
        {
            _className = GetType().Name;
        }

        protected Activity? StartActivity([CallerMemberName] string methodName = "")
        {
            return AppTelemetry.ActivitySource.StartActivity($"{_className}.{methodName}");
        }
    }
}
