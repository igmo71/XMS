using XMS.Integration.Abstractions;

namespace XMS.Integration.OneC.Common;

public record DocumentNotification : IIntegrationEvent
{
    public Guid Ref_Key { get; init; }
    public string? DataVersion { get; init; }
    public bool DeletionMark { get; init; }

    public bool Posted { get; init; }
    public string? Number { get; init; }
    public DateTime? Date { get; init; }

}
