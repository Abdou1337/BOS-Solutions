using BOS.Core.Identity;
using BOS.Core.MultiTenancy;

namespace BOS.Infrastructure.Identity;

/// <summary>
/// Default implementation of ICurrentUser (unauthenticated).
/// </summary>
public sealed class CurrentUser : ICurrentUser
{
    public UserId? UserId { get; set; }
    public string? Email { get; set; }
    public bool IsAuthenticated { get; set; }
    public IReadOnlyList<string> Roles { get; set; } = [];
}

/// <summary>
/// Default tenant context.
/// </summary>
public sealed class TenantContext : ITenantContext
{
    public TenantId? CurrentTenantId { get; set; }
}
