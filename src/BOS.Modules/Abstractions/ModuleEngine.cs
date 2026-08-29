using Microsoft.Extensions.DependencyInjection;

namespace BOS.Modules.Abstractions;

/// <summary>
/// Engine that discovers and loads modules.
/// </summary>
public sealed class ModuleEngine
{
    private readonly List<IBosModule> _modules = [];

    public IReadOnlyList<IBosModule> Modules => _modules.AsReadOnly();

    public ModuleEngine RegisterModule(IBosModule module)
    {
        _modules.Add(module);
        return this;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        foreach (var module in _modules)
        {
            module.RegisterServices(services);
        }
    }
}
