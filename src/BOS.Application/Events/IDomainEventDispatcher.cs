using BOS.Domain.Primitives;

namespace BOS.Application.Events;

/// <summary>
/// Application-level contract for dispatching domain events.
/// Implementation lives in Infrastructure.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
