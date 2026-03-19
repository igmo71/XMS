using XMS.Application.Common;
using XMS.Application.Common.Integration;

namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSNotifyService
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="notifyBody"></param>
     /// <returns></returns>
        Task<ServiceResult> NotifyAsync(OneSNotifyBody notifyBody);
    }
}
