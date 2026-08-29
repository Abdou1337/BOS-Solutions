namespace BOS.Core.Identity;

/// <summary>
/// Represents a user identifier.
/// </summary>
public sealed record UserId(Guid Value) : EntityId(Value)
{
    public static UserId New() => new(Guid.NewGuid());
}
