using System.ComponentModel.DataAnnotations;

namespace XMS.Core.Abstractions
{
    public abstract class Entity : IHasId<Guid>
    {
        public Guid Id { get; init; } = Guid.CreateVersion7();
    }
}
