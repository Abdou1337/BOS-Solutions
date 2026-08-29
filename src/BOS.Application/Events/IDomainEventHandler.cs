namespace BOS.Application.Events;

using BOS.Domain.Common;

/// <summary>
/// Handler for a specific domain event type.
/// </summary>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
