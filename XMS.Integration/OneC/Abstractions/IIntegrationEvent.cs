using XMS.Core.Abstractions.EventBus;

namespace XMS.Integration.OneC.Abstractions;

public interface IIntegrationEvent : IEvent
{
    Guid Ref_Key { get; set; }
    string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
}
