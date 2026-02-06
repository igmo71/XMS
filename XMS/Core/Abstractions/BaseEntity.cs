namespace XMS.Core.Abstractions
{
    public abstract class BaseEntity : IHasId<Guid>
    {
        public Guid Id { get; init; } = Guid.CreateVersion7();
    }
}
