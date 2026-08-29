namespace BOS.Domain.Primitives;

/// <summary>
/// Base class for domain entities with strongly-typed identifiers
/// and domain event support.
/// </summary>
public abstract class Entity<TId> where TId : EntityId
{
    public TId Id { get; protected set; } = default!;

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
