namespace XMS.Integration.OneC.Abstractions
{
    public interface IOneCCatalog
    {
        public Guid Ref_Key { get; set; }
        public string? DataVersion { get; set; }
        public string? Description { get; set; }
        public bool DeletionMark { get; set; }
    }
}
