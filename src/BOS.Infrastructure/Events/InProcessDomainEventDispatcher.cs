namespace BOS.Infrastructure.Events;

using BOS.Application.Events;
using BOS.Domain.Common;

/// <summary>
/// In-process domain event dispatcher that resolves handlers from a service provider.
/// Lifecycle: aggregate raises events → persistence saves → dispatcher invokes handlers.
/// </summary>
public sealed class InProcessDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InProcessDomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in events)
        {
            var eventType = domainEvent.GetType();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler is null) continue;

                var method = handlerType.GetMethod("HandleAsync")
                    ?? throw new InvalidOperationException($"HandleAsync not found on {handlerType.Name}");

                var task = method.Invoke(handler, [domainEvent, cancellationToken]) as Task
                    ?? throw new InvalidOperationException($"HandleAsync did not return a Task");

                await task;
            }
        }
    }
}

/// <summary>
/// Extension to simplify DI registration.
/// </summary>
public static class ServiceProviderExtensions
{
    internal static IEnumerable<object?> GetServices(this IServiceProvider provider, Type serviceType)
    {
        var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
        return (IEnumerable<object?>)(provider.GetService(enumerableType) ?? Array.Empty<object>());
    }
}
