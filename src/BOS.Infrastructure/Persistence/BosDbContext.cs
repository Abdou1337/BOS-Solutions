using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    private readonly IDomainEventDispatcher? _dispatcher;

    public BosDbContext(DbContextOptions<BosDbContext> options, IDomainEventDispatcher? dispatcher = null)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BosDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 1. Gather all domain events from tracked entities
        var domainEntities = ChangeTracker.Entries<IHasDomainEvents>()
            .Where(x => x.Entity.DomainEvents.Count > 0)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        // 2. Perform persistence
        var result = await base.SaveChangesAsync(cancellationToken);

        // 3. Dispatch events (if SaveChangesAsync didn't throw)
        if (domainEvents.Count > 0 && _dispatcher is not null)
        {
            await _dispatcher.DispatchAsync(domainEvents, cancellationToken);
        }

        // 4. Clear events only after successful dispatch/persistence
        foreach (var entity in domainEntities)
        {
            entity.Entity.ClearDomainEvents();
        }

        return result;
    }
}
