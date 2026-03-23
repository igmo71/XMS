namespace XMS.Integration.OneC
{
    public class OneCNotifyMessage
    {
        public Guid Ref_Key { get; set; }
        public string? DataVersion { get; set; }
        public bool DeletionMark { get; set; }
    }

    public class DocumentNotifyMessage : OneCNotifyMessage
    {
        public string? Number { get; set; }
        public DateTime Date { get; set; }
        public bool Posted { get; set; }
    }

    public class CatalogNotifyMessage : OneCNotifyMessage
    {
        public Guid Description { get; set; }
    }
}
