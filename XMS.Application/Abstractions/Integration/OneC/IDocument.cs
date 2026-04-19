namespace XMS.Application.Abstractions.Integration.OneC;

public interface IDocument : IOdataEntity
{
    bool Posted { get; set; }
    string? Number { get; set; }
    DateTime Date { get; set; }
}

public interface IOneCDocumentItem
{
    Guid Ref_Key { get; set; }
    int LineNumber { get; set; }
}
