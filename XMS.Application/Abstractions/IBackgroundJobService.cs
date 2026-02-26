using System.Linq.Expressions;

namespace XMS.Application.Abstractions
{
    internal interface IBackgroundJobService
    {
        string Enqueue<T>(Expression<Func<T, Task>> method);

        string Schedule<T>(Expression<Func<T, Task>> method, TimeSpan delay);

        void ContinueWith<T>(string parentJobId, Expression<Func<T, Task>> method);
    }
}
