using BOS.Core.Identity;
using BOS.Core.MultiTenancy;
using BOS.Domain.Users;
using FluentAssertions;

namespace BOS.Tests.Unit;

public class UserTests
{
    [Fact]
    public void Create_ShouldReturnUserWithDomainEvent()
    {
        var userId = UserId.New();
        var tenantId = TenantId.New();

        var user = User.Create(userId, "test@bos.com", "Test User", tenantId);

        user.Id.Should().Be(userId);
        user.Email.Should().Be("test@bos.com");
        user.DisplayName.Should().Be("Test User");
        user.TenantId.Should().Be(tenantId);
        user.IsActive.Should().BeTrue();
        user.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<UserCreatedEvent>();
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveFalse()
    {
        var user = User.Create(UserId.New(), "test@bos.com", "Test", TenantId.New());
        user.ClearDomainEvents();

        user.Deactivate();

        user.IsActive.Should().BeFalse();
        user.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<UserDeactivatedEvent>();
    }
}
