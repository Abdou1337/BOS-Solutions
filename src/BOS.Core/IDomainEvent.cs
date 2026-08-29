namespace BOS.Core;

/// <summary>
/// Represents a domain event.
/// </summary>
public interface IDomainEvent
{
    Guid EventId { get; }
    DateTimeOffset OccurredOn { get; }
}
