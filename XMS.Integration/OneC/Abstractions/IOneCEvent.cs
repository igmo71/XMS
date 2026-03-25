namespace XMS.Integration.OneC.Abstractions;

public interface IOneCEvent
{
    Guid Ref_Key { get; set; }
    string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
}
