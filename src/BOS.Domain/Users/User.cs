using BOS.Core;
using BOS.Core.Identity;
using BOS.Core.MultiTenancy;

namespace BOS.Domain.Users;

/// <summary>
/// Represents a platform user in the domain.
/// </summary>
public sealed class User : Entity<UserId>, IAggregateRoot, ITenantScoped
{
    public string Email { get; private set; } = default!;
    public string DisplayName { get; private set; } = default!;
    public TenantId TenantId { get; private set; } = default!;
    public DateTimeOffset CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private User() { }

    public static User Create(UserId id, string email, string displayName, TenantId tenantId)
    {
        var user = new User
        {
            Id = id,
            Email = email,
            DisplayName = displayName,
            TenantId = tenantId,
            CreatedAt = DateTimeOffset.UtcNow,
            IsActive = true
        };
        user.AddDomainEvent(new UserCreatedEvent(id));
        return user;
    }

    public void Deactivate()
    {
        IsActive = false;
        AddDomainEvent(new UserDeactivatedEvent(Id));
    }
}

public sealed record UserCreatedEvent(UserId UserId) : DomainEvent;
public sealed record UserDeactivatedEvent(UserId UserId) : DomainEvent;
