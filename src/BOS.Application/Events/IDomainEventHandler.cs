using BOS.Domain.Primitives;

namespace BOS.Application.Events;

/// <summary>
/// Application-level contract for handling a specific domain event type.
/// </summary>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
