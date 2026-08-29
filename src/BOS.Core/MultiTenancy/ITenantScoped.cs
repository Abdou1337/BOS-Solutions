namespace BOS.Core.MultiTenancy;

/// <summary>
/// Marker interface for entities scoped to a specific tenant.
/// Not all entities are tenant-scoped — the platform distinguishes:
/// - Platform-scoped entities (global, e.g. system configuration)
/// - Tenant-scoped entities (isolated per tenant)
/// - Organization-scoped entities (within a tenant's organization structure)
/// - Global reference data (shared across tenants, read-only)
/// </summary>
public interface ITenantScoped
{
    TenantId TenantId { get; }
}
