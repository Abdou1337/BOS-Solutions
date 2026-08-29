namespace BOS.Core.Authorization;

/// <summary>
/// Checks if the current user has a given permission.
/// </summary>
public interface IAuthorizationService
{
    Task<bool> HasPermissionAsync(string permission, CancellationToken cancellationToken = default);
}
