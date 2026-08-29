using System.Reflection;
using FluentAssertions;

namespace BOS.Tests.Architecture;

/// <summary>
/// Validates Clean Architecture dependency rules across all layers.
/// </summary>
public class CleanArchitectureTests
{
    private static readonly Assembly CoreAssembly = typeof(BOS.Core.Results.Result).Assembly;
    private static readonly Assembly DomainAssembly = typeof(BOS.Domain.Primitives.EntityId).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(BOS.Application.Abstractions.ICommand).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(BOS.Infrastructure.Persistence.BosDbContext).Assembly;
    private static readonly Assembly ModulesAssembly = typeof(BOS.Modules.Abstractions.IBosModule).Assembly;

    // --- Core layer: no dependency on any other BOS layer ---

    [Fact]
    public void Core_ShouldNotDependOn_Domain()
    {
        AssertNoDependency(CoreAssembly, "BOS.Domain");
    }

    [Fact]
    public void Core_ShouldNotDependOn_Application()
    {
        AssertNoDependency(CoreAssembly, "BOS.Application");
    }

    [Fact]
    public void Core_ShouldNotDependOn_Infrastructure()
    {
        AssertNoDependency(CoreAssembly, "BOS.Infrastructure");
    }

    [Fact]
    public void Core_ShouldNotDependOn_Api()
    {
        AssertNoDependency(CoreAssembly, "BOS.Api");
    }

    [Fact]
    public void Core_ShouldNotDependOn_Desktop()
    {
        AssertNoDependency(CoreAssembly, "BOS.Desktop");
    }

    // --- Domain layer: only depends on Core ---

    [Fact]
    public void Domain_ShouldNotDependOn_Application()
    {
        AssertNoDependency(DomainAssembly, "BOS.Application");
    }

    [Fact]
    public void Domain_ShouldNotDependOn_Infrastructure()
    {
        AssertNoDependency(DomainAssembly, "BOS.Infrastructure");
    }

    [Fact]
    public void Domain_ShouldNotDependOn_Api()
    {
        AssertNoDependency(DomainAssembly, "BOS.Api");
    }

    [Fact]
    public void Domain_ShouldNotDependOn_EFCore()
    {
        AssertNoDependency(DomainAssembly, "Microsoft.EntityFrameworkCore");
    }

    // --- Application layer: depends on Domain + Core only ---

    [Fact]
    public void Application_ShouldNotDependOn_Infrastructure()
    {
        AssertNoDependency(ApplicationAssembly, "BOS.Infrastructure");
    }

    [Fact]
    public void Application_ShouldNotDependOn_Api()
    {
        AssertNoDependency(ApplicationAssembly, "BOS.Api");
    }

    [Fact]
    public void Application_ShouldNotDependOn_EFCore()
    {
        AssertNoDependency(ApplicationAssembly, "Microsoft.EntityFrameworkCore");
    }

    // --- Modules: must not require concrete business modules ---

    [Fact]
    public void Modules_ShouldNotDependOn_Infrastructure()
    {
        AssertNoDependency(ModulesAssembly, "BOS.Infrastructure");
    }

    [Fact]
    public void Modules_ShouldNotDependOn_Api()
    {
        AssertNoDependency(ModulesAssembly, "BOS.Api");
    }

    [Fact]
    public void Modules_ShouldNotDependOn_EFCore()
    {
        AssertNoDependency(ModulesAssembly, "Microsoft.EntityFrameworkCore");
    }

    // --- No circular dependencies ---

    [Fact]
    public void Infrastructure_ShouldNotDependOn_Api()
    {
        AssertNoDependency(InfrastructureAssembly, "BOS.Api");
    }

    private static void AssertNoDependency(Assembly assembly, string forbiddenAssemblyName)
    {
        assembly.GetReferencedAssemblies()
            .Should().NotContain(
                r => r.Name == forbiddenAssemblyName,
                $"{assembly.GetName().Name} must not reference {forbiddenAssemblyName}");
    }
}
