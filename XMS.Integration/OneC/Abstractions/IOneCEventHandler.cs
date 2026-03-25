using XMS.Core.Common;

namespace XMS.Integration.OneC.Abstractions;

public interface IOneCEventHandler<TEvent> where TEvent : class, IOneCEvent
{
    Task<ServiceResult> HandleEventOneC(TEvent oneCNotifyMessage, CancellationToken ct = default);
}
