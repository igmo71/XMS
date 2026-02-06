namespace XMS.Core.Abstractions
{
    public abstract class BaseEntity : IHasId<Guid>
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
    }
}
