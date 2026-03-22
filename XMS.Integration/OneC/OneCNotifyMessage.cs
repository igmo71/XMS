namespace XMS.Integration.OneC
{
    public class OneCNotifyMessage
    {
        public Guid Ref_Key { get; set; }
        public bool DeletionMark { get; set; }
        public string? Number { get; set; }
        public DateTime Date { get; set; }
        public bool Posted { get; set; }
    }
}
