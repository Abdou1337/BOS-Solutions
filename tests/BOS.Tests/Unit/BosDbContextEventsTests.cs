using BOS.Application.Events;
using BOS.Domain.Primitives;
using BOS.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BOS.Tests.Unit;

public class BosDbContextEventsTests
{
    private sealed record TestId(Guid Value) : EntityId(Value);

    private sealed class TestDbEntity : Entity<TestId>
    {
        public string Name { get; set; } = string.Empty;

        public static TestDbEntity Create(string name)
        {
            var entity = new TestDbEntity { Id = new TestId(Guid.NewGuid()), Name = name };
            entity.AddDomainEvent(new TestEvent(name));
            return entity;
        }

        public static TestDbEntity CreateWithId(TestId id, string name)
        {
            var entity = new TestDbEntity { Id = id, Name = name };
            entity.AddDomainEvent(new TestEvent(name));
            return entity;
        }
    }

    private sealed record TestEvent(string Name) : DomainEvent;

    private sealed class FakeDispatcher : IDomainEventDispatcher
    {
        public List<IDomainEvent> DispatchedEvents { get; } = [];
        public bool ShouldThrow { get; set; }

        public Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            if (ShouldThrow)
            {
                throw new InvalidOperationException("Dispatch failed");
            }
            DispatchedEvents.Add(domainEvent);
            return Task.CompletedTask;
        }

        public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                DispatchedEvents.Add(domainEvent);
            }
            return Task.CompletedTask;
        }
    }

    private sealed class TestBosDbContext : BosDbContext
    {
        public DbSet<TestDbEntity> TestEntities => Set<TestDbEntity>();

        public TestBosDbContext(DbContextOptions<BosDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options, dispatcher)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestDbEntity>(cfg =>
            {
                cfg.HasKey(e => e.Id);
                cfg.Property(e => e.Id).HasConversion(id => id.Value, value => new TestId(value));
            });
        }
    }

    [Fact]
    public async Task SaveChangesAsync_OnSuccessfulPersistence_DispatchesAndClearsEvents()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<BosDbContext>()
            .UseSqlite(connection)
            .Options;

        var dispatcher = new FakeDispatcher();

        using (var context = new TestBosDbContext(options, dispatcher))
        {
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = new TestBosDbContext(options, dispatcher))
        {
            var entity = TestDbEntity.Create("PersistedEntity");
            context.TestEntities.Add(entity);

            // Act
            var result = await context.SaveChangesAsync();

            // Assert
            result.Should().Be(1);
            dispatcher.DispatchedEvents.Should().ContainSingle();
            dispatcher.DispatchedEvents[0].As<TestEvent>().Name.Should().Be("PersistedEntity");
            entity.DomainEvents.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task SaveChangesAsync_OnFailedPersistence_DoesNotDispatchAndDoesNotClearEvents()
    {
        // Arrange
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<BosDbContext>()
            .UseSqlite(connection)
            .Options;

        var dispatcher = new FakeDispatcher();

        using (var context = new TestBosDbContext(options, dispatcher))
        {
            await context.Database.EnsureCreatedAsync();
        }

        TestId entityId;
        using (var context = new TestBosDbContext(options, dispatcher))
        {
            var entity = TestDbEntity.Create("DuplicateEntity");
            entityId = entity.Id;
            context.TestEntities.Add(entity);
            await context.SaveChangesAsync();
            dispatcher.DispatchedEvents.Clear();
        }

        using (var context = new TestBosDbContext(options, dispatcher))
        {
            // Try to add entity with same key to trigger DbUpdateException (uniqueness failure)
            var duplicateEntity = TestDbEntity.CreateWithId(entityId, "DuplicateEntity2");
            context.TestEntities.Add(duplicateEntity);

            // Act
            Func<Task> act = async () => await context.SaveChangesAsync();

            // Assert
            await act.Should().ThrowAsync<DbUpdateException>();
            dispatcher.DispatchedEvents.Should().BeEmpty();
            duplicateEntity.DomainEvents.Should().ContainSingle();
        }
    }
}
