namespace BOS.Core.MultiTenancy;

/// <summary>
/// Provides access to the current tenant context.
/// </summary>
public interface ITenantContext
{
    TenantId? CurrentTenantId { get; }
}
