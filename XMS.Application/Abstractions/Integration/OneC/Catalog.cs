namespace XMS.Application.Abstractions.Integration.OneC;

public abstract class Catalog : IOdataEntity
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public string? Description { get; set; }
}
