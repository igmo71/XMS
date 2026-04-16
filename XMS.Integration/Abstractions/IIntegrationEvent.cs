using XMS.Core.Abstractions.EventBus;

namespace XMS.Integration.Abstractions;

public interface IIntegrationEvent : IEvent
{
    Guid Ref_Key { get; init; }
    string? DataVersion { get; init; }
    public bool DeletionMark { get; init; }
}
