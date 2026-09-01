using BOS.Application.Events;
using BOS.Application.Persistence;
using BOS.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace BOS.Infrastructure.Persistence;

/// <summary>
/// Foundation-level EF Core DbContext for BOS platform.
/// Currently minimal — future phases will add entity configurations
/// per bounded context/module.
/// </summary>
public class BosDbContext : DbContext, IUnitOfWork
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public BosDbContext(
        DbContextOptions<BosDbContext> options,
        IDomainEventDispatcher domainEventDispatcher)
        : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BosDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 1. Get all entities that implement IEntityWithDomainEvents and have domain events
        var entitiesWithEvents = ChangeTracker.Entries<IEntityWithDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToList();

        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // 2. Persist changes to the database
        var result = await base.SaveChangesAsync(cancellationToken);

        // 3. Dispatch events after successful persistence
        if (domainEvents.Count > 0)
        {
            await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);

            // 4. Clear events only after successful persistence and dispatch
            foreach (var entity in entitiesWithEvents)
            {
                entity.ClearDomainEvents();
            }
        }

        return result;
    }
}
