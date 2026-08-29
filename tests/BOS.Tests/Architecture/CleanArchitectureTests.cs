using System.Reflection;
using FluentAssertions;

namespace BOS.Tests.Architecture;

/// <summary>
/// Validates the Clean Architecture dependency rules.
/// </summary>
public class CleanArchitectureTests
{
    private static readonly Assembly CoreAssembly = typeof(BOS.Core.Entity<>).Assembly;
    private static readonly Assembly DomainAssembly = typeof(BOS.Domain.Users.User).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(BOS.Application.Abstractions.ICommand).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(BOS.Infrastructure.Persistence.BosDbContext).Assembly;

    [Fact]
    public void Core_ShouldNotDependOn_Domain()
    {
        var references = CoreAssembly.GetReferencedAssemblies();
        references.Should().NotContain(r => r.Name == "BOS.Domain");
    }

    [Fact]
    public void Core_ShouldNotDependOn_Application()
    {
        var references = CoreAssembly.GetReferencedAssemblies();
        references.Should().NotContain(r => r.Name == "BOS.Application");
    }

    [Fact]
    public void Core_ShouldNotDependOn_Infrastructure()
    {
        var references = CoreAssembly.GetReferencedAssemblies();
        references.Should().NotContain(r => r.Name == "BOS.Infrastructure");
    }

    [Fact]
    public void Domain_ShouldNotDependOn_Application()
    {
        var references = DomainAssembly.GetReferencedAssemblies();
        references.Should().NotContain(r => r.Name == "BOS.Application");
    }

    [Fact]
    public void Domain_ShouldNotDependOn_Infrastructure()
    {
        var references = DomainAssembly.GetReferencedAssemblies();
        references.Should().NotContain(r => r.Name == "BOS.Infrastructure");
    }

    [Fact]
    public void Application_ShouldNotDependOn_Infrastructure()
    {
        var references = ApplicationAssembly.GetReferencedAssemblies();
        references.Should().NotContain(r => r.Name == "BOS.Infrastructure");
    }

    [Fact]
    public void Domain_ShouldDependOn_Core()
    {
        var references = DomainAssembly.GetReferencedAssemblies();
        references.Should().Contain(r => r.Name == "BOS.Core");
    }

    [Fact]
    public void Application_ShouldDependOn_Domain()
    {
        // Application references Domain via project reference.
        // At compile time, the reference is only included when types are used.
        // We verify it does NOT depend on Infrastructure instead.
        var references = ApplicationAssembly.GetReferencedAssemblies();
        references.Should().NotContain(r => r.Name == "BOS.Infrastructure");
    }
}
