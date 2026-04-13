namespace XMS.Integration.OneC.Abstractions;

public interface ICatalog
{
    Guid Ref_Key { get; set; }
    string? DataVersion { get; set; }
    bool DeletionMark { get; set; }
    string? Description { get; set; }
}
