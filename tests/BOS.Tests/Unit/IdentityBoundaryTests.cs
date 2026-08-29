using BOS.Core.Identity;
using BOS.Infrastructure.Identity;
using FluentAssertions;

namespace BOS.Tests.Unit;

public class IdentityBoundaryTests
{
    [Fact]
    public void UserId_New_CreatesUniqueIds()
    {
        var id1 = UserId.New();
        var id2 = UserId.New();

        id1.Should().NotBe(id2);
    }

    [Fact]
    public void CurrentUser_DefaultIsUnauthenticated()
    {
        var user = new CurrentUser();

        user.IsAuthenticated.Should().BeFalse();
        user.UserId.Should().BeNull();
        user.Email.Should().BeNull();
        user.Roles.Should().BeEmpty();
    }
}
