namespace BOS.Core.Identity;

/// <summary>
/// Provides access to the current user context.
/// </summary>
public interface ICurrentUser
{
    UserId? UserId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    IReadOnlyList<string> Roles { get; }
}
