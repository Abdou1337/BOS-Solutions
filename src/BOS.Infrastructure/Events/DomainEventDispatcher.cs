using BOS.Application.Events;
using BOS.Domain.Primitives;
using Microsoft.Extensions.DependencyInjection;

namespace BOS.Infrastructure.Events;

/// <summary>
/// In-process domain event dispatcher that resolves handlers from DI.
/// This is a foundation-level implementation for in-process dispatch only.
/// Future phases may add outbox patterns and integration event support.
/// </summary>
public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            if (handler is null) continue;
            var method = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync));
            if (method is not null)
            {
                var task = (Task?)method.Invoke(handler, [domainEvent, cancellationToken]);
                if (task is not null) await task;
            }
        }
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await DispatchAsync(domainEvent, cancellationToken);
        }
    }
}
