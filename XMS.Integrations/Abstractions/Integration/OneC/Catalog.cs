namespace XMS.Integrations.OneC;

public abstract class Catalog : IOdataEntity, ISelectable
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public string? Description { get; set; }
}
