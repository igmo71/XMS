namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCCatalog
    {
        public Guid Ref_Key { get; set; }
        public string? DataVersion { get; set; }
        public string? Description { get; set; }
        public bool DeletionMark { get; set; }

        static abstract string Uri { get; }
        static abstract string GetUriByRefKey(Guid refKey);
        static abstract string QueueName { get; }
    }
}
