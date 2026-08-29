using BOS.Domain.Primitives;
using FluentAssertions;

namespace BOS.Tests.Unit;

public class DomainPrimitivesTests
{
    private sealed record TestId(Guid Value) : EntityId(Value);

    private sealed class TestEntity : Entity<TestId>
    {
        public static TestEntity Create(TestId id)
        {
            var entity = new TestEntity { Id = id };
            return entity;
        }
    }

    private sealed record TestEvent : DomainEvent;

    [Fact]
    public void EntityId_ToString_ReturnsGuidString()
    {
        var guid = Guid.NewGuid();
        var id = new TestId(guid);
        id.ToString().Should().Be(guid.ToString());
    }

    [Fact]
    public void Entity_ShouldTrackDomainEvents()
    {
        var entity = TestEntity.Create(new TestId(Guid.NewGuid()));
        var evt = new TestEvent();

        entity.AddDomainEvent(evt);

        entity.DomainEvents.Should().ContainSingle().Which.Should().Be(evt);
    }

    [Fact]
    public void Entity_ClearDomainEvents_ShouldRemoveAll()
    {
        var entity = TestEntity.Create(new TestId(Guid.NewGuid()));
        entity.AddDomainEvent(new TestEvent());
        entity.AddDomainEvent(new TestEvent());

        entity.ClearDomainEvents();

        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void DomainEvent_ShouldHaveIdAndTimestamp()
    {
        var evt = new TestEvent();

        evt.EventId.Should().NotBeEmpty();
        evt.OccurredOn.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(5));
    }
}
