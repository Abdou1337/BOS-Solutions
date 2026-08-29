namespace BOS.Core.MultiTenancy;

/// <summary>
/// Represents a tenant identifier in the platform.
/// This is a cross-cutting multi-tenancy concept, not a domain entity.
/// </summary>
public readonly record struct TenantId(Guid Value)
{
    public static TenantId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}
