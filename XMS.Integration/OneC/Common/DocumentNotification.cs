using XMS.Integration.Abstractions;

namespace XMS.Integration.OneC.Common;

public class DocumentNotification : IIntegrationEvent
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }

    public bool Posted { get; set; }
    public string? Number { get; set; }
    public DateTime? Date { get; set; }

}
