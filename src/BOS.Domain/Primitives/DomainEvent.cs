namespace BOS.Domain.Primitives;

/// <summary>
/// Base record for domain events providing default identity and timestamp.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
}
