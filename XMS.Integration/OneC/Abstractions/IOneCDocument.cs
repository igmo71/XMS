namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCDocument
    {
        string Ref_Key { get; set; }
        string? Number { get; set; }
        DateTime Date { get; set; }
    }

    public interface IOneCDocumentItem
    {
        string Ref_Key { get; set; }
        int LineNumber { get; set; }
    }
}
