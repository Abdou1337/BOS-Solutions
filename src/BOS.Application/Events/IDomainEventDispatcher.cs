namespace BOS.Application.Events;

using BOS.Domain.Common;

/// <summary>
/// Dispatches domain events to registered handlers after the persistence boundary.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default);
}
