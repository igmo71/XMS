namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCCatalog
    {
        Guid Ref_Key { get; set; }
        string? DataVersion { get; set; }
        bool DeletionMark { get; set; }
        string? Description { get; set; }

        static abstract string Uri { get; }
        static abstract string GetUriByRefKey(Guid refKey);
        static abstract string GetUriByDate(DateTime? from = null, DateTime? to = null);
        static abstract string QueueName { get; }
    }
}
