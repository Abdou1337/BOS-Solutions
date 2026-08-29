namespace BOS.Core.Authorization;

/// <summary>
/// Represents a permission in the authorization system.
/// </summary>
public sealed record Permission(string Name, string? Description = null);
