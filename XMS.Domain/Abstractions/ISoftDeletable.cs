namespace XMS.Domain.Abstractions
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
    }
}
