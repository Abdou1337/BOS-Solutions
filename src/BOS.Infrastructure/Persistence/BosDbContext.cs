using BOS.Core.Events;
using BOS.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BOS.Infrastructure.Persistence;

/// <summary>
/// Main EF Core DbContext for BOS platform.
/// </summary>
public sealed class BosDbContext : DbContext, IUnitOfWork
{
    private readonly IDomainEventDispatcher? _eventDispatcher;

    public BosDbContext(DbContextOptions<BosDbContext> options, IDomainEventDispatcher? eventDispatcher = null)
        : base(options)
    {
        _eventDispatcher = eventDispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BosDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        // Domain event dispatching would happen here in a full implementation
        return result;
    }
}
