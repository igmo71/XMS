namespace XMS.Core.Abstractions
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
