using System.Diagnostics;
using System.Runtime.CompilerServices;
using XMS.Application.Common;

namespace XMS.Application.Abstractions
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
