using BOS.Core.MultiTenancy;
using BOS.Infrastructure.Identity;
using FluentAssertions;

namespace BOS.Tests.Unit;

public class TenantContextTests
{
    [Fact]
    public void TenantId_New_CreatesUniqueIds()
    {
        var id1 = TenantId.New();
        var id2 = TenantId.New();

        id1.Should().NotBe(id2);
    }

    [Fact]
    public void TenantContext_DefaultIsNull()
    {
        var ctx = new TenantContext();
        ctx.CurrentTenantId.Should().BeNull();
    }

    [Fact]
    public void TenantContext_CanBeSet()
    {
        var tenantId = TenantId.New();
        var ctx = new TenantContext { CurrentTenantId = tenantId };

        ctx.CurrentTenantId.Should().Be(tenantId);
    }
}
