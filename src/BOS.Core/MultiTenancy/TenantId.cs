namespace BOS.Core.MultiTenancy;

/// <summary>
/// Represents a tenant identifier.
/// </summary>
public sealed record TenantId(Guid Value) : EntityId(Value)
{
    public static TenantId New() => new(Guid.NewGuid());
}
