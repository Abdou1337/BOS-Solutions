namespace BOS.Core.Identity;

/// <summary>
/// Represents an authenticated user's identifier in the platform.
/// This is a cross-cutting identity concept, not a domain entity.
/// </summary>
public readonly record struct UserId(Guid Value)
{
    public static UserId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}
