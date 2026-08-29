namespace BOS.Domain.Tests;

using BOS.Application.Events;
using BOS.Domain.Common;
using BOS.Domain.Events;
using BOS.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class DomainEventLifecycleTests
{
    [Fact]
    public async Task FullLifecycle_AggregateToHandler_Works()
    {
        // Arrange: register handler
        var services = new ServiceCollection();
        services.AddSingleton<IDomainEventHandler<SampleEntityCreatedEvent>, TestHandler>();
        var provider = services.BuildServiceProvider();
        var dispatcher = new InProcessDomainEventDispatcher(provider);

        // Act: aggregate raises event → simulate persistence → dispatch
        var aggregate = TestAggregate.Create();
        var events = aggregate.DomainEvents.ToList();
        aggregate.ClearDomainEvents(); // persistence boundary

        await dispatcher.DispatchAsync(events);

        // Assert: handler was invoked
        var handler = provider.GetServices<IDomainEventHandler<SampleEntityCreatedEvent>>()
            .OfType<TestHandler>().Single();
        Assert.True(handler.WasHandled);
    }

    private sealed class TestHandler : IDomainEventHandler<SampleEntityCreatedEvent>
    {
        public bool WasHandled { get; private set; }

        public Task HandleAsync(SampleEntityCreatedEvent domainEvent, CancellationToken cancellationToken = default)
        {
            WasHandled = true;
            return Task.CompletedTask;
        }
    }
}
