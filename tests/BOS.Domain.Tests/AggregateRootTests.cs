namespace BOS.Domain.Tests;

using BOS.Domain.Common;
using BOS.Domain.Events;
using Xunit;

public sealed class TestAggregate : AggregateRoot
{
    public Guid Id { get; }

    private TestAggregate(Guid id) => Id = id;

    public static TestAggregate Create()
    {
        var aggregate = new TestAggregate(Guid.NewGuid());
        aggregate.RaiseDomainEvent(new SampleEntityCreatedEvent(aggregate.Id));
        return aggregate;
    }
}

public class AggregateRootTests
{
    [Fact]
    public void Create_RaisesDomainEvent()
    {
        var aggregate = TestAggregate.Create();

        Assert.Single(aggregate.DomainEvents);
        Assert.IsType<SampleEntityCreatedEvent>(aggregate.DomainEvents[0]);
    }

    [Fact]
    public void ClearDomainEvents_RemovesAllEvents()
    {
        var aggregate = TestAggregate.Create();
        aggregate.ClearDomainEvents();

        Assert.Empty(aggregate.DomainEvents);
    }
}
