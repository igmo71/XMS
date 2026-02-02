using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XMS.Core.Abstractions
{
    public abstract class BaseService
    {
        protected readonly string _className;

        protected BaseService()
        {
            _className = GetType().Name;
        }

        protected Activity? StartActivity([CallerMemberName] string methodName = "")
        {
            return AppTelemetry.ActivitySource.StartActivity($"{_className}.{methodName}");
        }
    }
}
