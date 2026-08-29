namespace BOS.Core.MultiTenancy;

/// <summary>
/// Marker interface for tenant-aware entities.
/// </summary>
public interface ITenantScoped
{
    TenantId TenantId { get; }
}
