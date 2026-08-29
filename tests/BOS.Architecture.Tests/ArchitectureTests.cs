namespace BOS.Architecture.Tests;

using NetArchTest.Rules;
using Xunit;

public class DependencyTests
{
    [Fact]
    public void Domain_ShouldNotDependOnApplication()
    {
        var result = Types.InAssembly(typeof(BOS.Domain.Common.IDomainEvent).Assembly)
            .ShouldNot()
            .HaveDependencyOn("BOS.Application")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Domain_ShouldNotDependOnInfrastructure()
    {
        var result = Types.InAssembly(typeof(BOS.Domain.Common.IDomainEvent).Assembly)
            .ShouldNot()
            .HaveDependencyOn("BOS.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Application_ShouldNotDependOnInfrastructure()
    {
        var result = Types.InAssembly(typeof(BOS.Application.Events.IDomainEventDispatcher).Assembly)
            .ShouldNot()
            .HaveDependencyOn("BOS.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
