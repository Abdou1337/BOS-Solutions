namespace BOS.Domain.Primitives;

/// <summary>
/// Represents a domain event raised by an aggregate.
/// </summary>
public interface IDomainEvent
{
    Guid EventId { get; }
    DateTimeOffset OccurredOn { get; }
}
