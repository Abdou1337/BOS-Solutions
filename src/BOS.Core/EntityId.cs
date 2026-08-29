namespace BOS.Core;

/// <summary>
/// Strongly-typed identifier base for all entities.
/// </summary>
public abstract record EntityId(Guid Value)
{
    public override string ToString() => Value.ToString();
}
