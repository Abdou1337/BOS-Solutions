using BOS.Modules.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BOS.Tests.Unit;

public class ModuleEngineTests
{
    [Fact]
    public void RegisterModule_ShouldAddModule()
    {
        var engine = new ModuleEngine();
        var module = new TestModule();

        engine.RegisterModule(module);

        engine.Modules.Should().ContainSingle()
            .Which.Name.Should().Be("Test");
    }

    [Fact]
    public void ConfigureServices_ShouldCallAllModules()
    {
        var engine = new ModuleEngine();
        var module = new TestModule();
        engine.RegisterModule(module);
        var services = new ServiceCollection();

        engine.ConfigureServices(services);

        module.WasConfigured.Should().BeTrue();
    }

    private sealed class TestModule : IBosModule
    {
        public string Name => "Test";
        public string Description => "Test module";
        public string Version => "1.0.0";
        public bool WasConfigured { get; private set; }

        public IServiceCollection RegisterServices(IServiceCollection services)
        {
            WasConfigured = true;
            return services;
        }
    }
}
