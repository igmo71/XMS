namespace XMS.Integrations.OneC;

public interface IOdataEntity
{
    Guid Ref_Key { get; set; }
    string? DataVersion { get; set; }
    bool DeletionMark { get; set; }
}
