using BOS.Application.Events;
using BOS.Application.Persistence;
using BOS.Core.Identity;
using BOS.Core.MultiTenancy;
using BOS.Infrastructure.Events;
using BOS.Infrastructure.Identity;
using BOS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BOS.Infrastructure.DependencyInjection;

/// <summary>
/// Registers infrastructure layer services.
/// Foundation-level registration — persistence provider selection is
/// intentionally simple at this stage. Future phases will introduce
/// separate local/cloud persistence boundaries and sync engine.
/// </summary>
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string connectionString,
        bool useSqlite = false)
    {
        services.AddDbContext<BosDbContext>(options =>
        {
            if (useSqlite)
                options.UseSqlite(connectionString);
            else
                options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<BosDbContext>());
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ITenantContext, TenantContext>();

        return services;
    }
}
