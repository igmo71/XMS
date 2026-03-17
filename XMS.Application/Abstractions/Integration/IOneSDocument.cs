namespace XMS.Application.Abstractions.Integration
{
    public interface IOneSDocument
    {
        string Ref_Key { get; set; }
        string? Number { get; set; }
        DateTime Date { get; set; }
    }

    public interface IOneSDocumentItem
    {
        string Ref_Key { get; set; }
        int LineNumber { get; set; }
    }
}
