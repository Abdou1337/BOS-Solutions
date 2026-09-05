namespace BOS.Domain;

/// <summary>
/// Base class for all domain entities that can raise domain events.
/// </summary>
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}

/// <summary>
/// Marker interface for domain events.
/// </summary>
public interface IDomainEvent { }
