namespace BOS.Core.Authorization;

/// <summary>
/// Platform-level authorization boundary.
/// Checks whether the current user has a specific permission
/// in their current tenant/organization context.
/// 
/// Future phases will implement the full authorization chain:
/// User → TenantMembership → Role → Permission → Policy → Resource
/// </summary>
public interface IAuthorizationService
{
    Task<bool> HasPermissionAsync(string permission, CancellationToken cancellationToken = default);
}
