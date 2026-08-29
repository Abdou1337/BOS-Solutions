namespace BOS.Domain.Events;

using BOS.Domain.Common;

/// <summary>
/// Example domain event raised when a sample entity is created.
/// </summary>
public sealed class SampleEntityCreatedEvent : IDomainEvent
{
    public Guid EntityId { get; }
    public DateTime OccurredOn { get; }

    public SampleEntityCreatedEvent(Guid entityId)
    {
        EntityId = entityId;
        OccurredOn = DateTime.UtcNow;
    }
}
