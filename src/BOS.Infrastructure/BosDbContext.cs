using BOS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BOS.Infrastructure;

/// <summary>
/// EF Core DbContext for BOS Solutions.
/// Dispatches domain events raised by <see cref="Entity"/> aggregates
/// after successfully persisting changes (unit-of-work boundary).
/// </summary>
public class BosDbContext : DbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public BosDbContext(
        DbContextOptions<BosDbContext> options,
        IDomainEventDispatcher? dispatcher = null)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    // ── DbSets ──────────────────────────────────────────────────────────────
    // Add your domain DbSets here, e.g.:
    // public DbSet<Order> Orders => Set<Order>();

    // ── SaveChanges overrides ────────────────────────────────────────────────

    /// <inheritdoc />
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        DispatchDomainEventsAsync().GetAwaiter().GetResult();
        return result;
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await DispatchDomainEventsAsync(cancellationToken);
        return result;
    }

    // ── Model configuration ──────────────────────────────────────────────────

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BosDbContext).Assembly);
    }

    // ── Domain-event dispatch ────────────────────────────────────────────────

    private async Task DispatchDomainEventsAsync(CancellationToken ct = default)
    {
        if (_dispatcher is null) return;

        var entities = ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .Select(e => e.Entity)
            .ToList();

        foreach (var entity in entities)
        {
            var events = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
                await _dispatcher.DispatchAsync(domainEvent, ct);
        }
    }
}

/// <summary>
/// Abstraction for dispatching domain events outside the persistence layer.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
