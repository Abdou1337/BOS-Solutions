namespace BOS.Domain.Primitives;

/// <summary>
/// Strongly-typed identifier base for all domain entities.
/// </summary>
public abstract record EntityId(Guid Value)
{
    public sealed override string ToString() => Value.ToString();
}
