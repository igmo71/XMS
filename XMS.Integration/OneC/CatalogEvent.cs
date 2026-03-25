using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC;

public class CatalogEvent : IOneCEvent
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }

    public string? Description { get; set; }
}
