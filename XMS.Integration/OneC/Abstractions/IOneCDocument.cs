namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCDocument
    {
        Guid Ref_Key { get; set; }
        string? Number { get; set; }
        DateTime Date { get; set; }
    }

    public interface IOneCDocumentItem
    {
        Guid Ref_Key { get; set; }
        int LineNumber { get; set; }
    }
}
