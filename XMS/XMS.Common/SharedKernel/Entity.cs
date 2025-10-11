using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel;

public abstract class Entity : IHasId<Guid>
{
    public Guid Id { get; protected set; }

    private List<IEvent>? _domainEvents;
    public IReadOnlyCollection<IEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(IEvent eventItem)
    {
        _domainEvents ??= [];
        _domainEvents.Add(eventItem);
    }
}
