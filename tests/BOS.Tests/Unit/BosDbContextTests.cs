using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BOS.Application.Events;
using BOS.Domain.Primitives;
using BOS.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BOS.Tests.Unit;

public class BosDbContextTests
{
    private sealed record TestId(Guid Value) : EntityId(Value);

    private sealed class TestEntity : Entity<TestId>
    {
        public string Name { get; set; } = string.Empty;

        public static TestEntity Create(string name)
        {
            var entity = new TestEntity { Id = new TestId(Guid.NewGuid()), Name = name };
            return entity;
        }
    }

    private sealed record TestEvent : DomainEvent;

    private sealed class TestBosDbContext : BosDbContext
    {
        public TestBosDbContext(DbContextOptions<BosDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options, dispatcher)
        {
        }

        public DbSet<TestEntity> TestEntities => Set<TestEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestEntity>(builder =>
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.Id)
                    .HasConversion(id => id.Value, value => new TestId(value));
                builder.Property(e => e.Name).IsRequired();
            });
        }
    }

    private sealed class FakeDomainEventDispatcher : IDomainEventDispatcher
    {
        public List<IDomainEvent> DispatchedEvents { get; } = [];

        public Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            DispatchedEvents.Add(domainEvent);
            return Task.CompletedTask;
        }

        public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            DispatchedEvents.AddRange(domainEvents);
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldDispatchAndClearEvents_WhenPersistenceIsSuccessful()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<BosDbContext>()
            .UseSqlite(connection)
            .Options;

        var dispatcher = new FakeDomainEventDispatcher();
        using var context = new TestBosDbContext(options, dispatcher);
        await context.Database.EnsureCreatedAsync();

        var entity = TestEntity.Create("Test Entity");
        var domainEvent = new TestEvent();
        entity.AddDomainEvent(domainEvent);

        context.TestEntities.Add(entity);

        // Act
        var result = await context.SaveChangesAsync();

        // Assert
        result.Should().BeGreaterThan(0);
        dispatcher.DispatchedEvents.Should().ContainSingle().Which.Should().Be(domainEvent);
        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldNotDispatchOrClearEvents_WhenPersistenceFails()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<BosDbContext>()
            .UseSqlite(connection)
            .Options;

        var dispatcher = new FakeDomainEventDispatcher();
        using var context = new TestBosDbContext(options, dispatcher);
        await context.Database.EnsureCreatedAsync();

        var entity = TestEntity.Create(null!); // Name is required, so this will fail validation/persistence
        var domainEvent = new TestEvent();
        entity.AddDomainEvent(domainEvent);

        context.TestEntities.Add(entity);

        // Act
        Func<Task> saveAction = async () => await context.SaveChangesAsync();

        // Assert
        await saveAction.Should().ThrowAsync<DbUpdateException>();
        dispatcher.DispatchedEvents.Should().BeEmpty();
        entity.DomainEvents.Should().ContainSingle().Which.Should().Be(domainEvent);
    }
}
