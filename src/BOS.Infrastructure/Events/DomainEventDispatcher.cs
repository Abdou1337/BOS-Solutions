using BOS.Core;
using BOS.Core.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BOS.Infrastructure.Events;

/// <summary>
/// In-process domain event dispatcher using DI-resolved handlers.
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

    private static IEnumerable<object?> GetServices(IServiceProvider provider, Type serviceType)
    {
        var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
        return (IEnumerable<object?>?)provider.GetService(enumerableType) ?? [];
    }
}
