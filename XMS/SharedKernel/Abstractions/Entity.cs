using System.ComponentModel.DataAnnotations;

namespace XMS.SharedKernel.Abstractions
{
    public abstract class Entity : IHasId<Guid>
    {
        public Guid Id { get; protected set; }
    }
}
