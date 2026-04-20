namespace XMS.Application.Abstractions.Integration.OneC;

public abstract class Document : IOdataEntity
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public bool Posted { get; set; }
    public string? Number { get; set; }
    public DateTime Date { get; set; }
}

public interface IOneCDocumentItem
{
    Guid Ref_Key { get; set; }
    int LineNumber { get; set; }
}
