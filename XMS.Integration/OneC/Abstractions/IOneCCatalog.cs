namespace XMS.Integration.OneC.Abstractions
{
    internal interface IOneCCatalog
    {
        public Guid Ref_Key { get; set; }
        public string? Description { get; set; }
        public bool DeletionMark { get; set; }
    }
}
