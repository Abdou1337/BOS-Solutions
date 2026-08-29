using BOS.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BOS.Infrastructure.Persistence;

/// <summary>
/// Foundation-level EF Core DbContext for BOS platform.
/// Currently minimal — future phases will add entity configurations
/// per bounded context/module.
/// </summary>
public sealed class BosDbContext : DbContext, IUnitOfWork
{
    public BosDbContext(DbContextOptions<BosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BosDbContext).Assembly);
    }
}
