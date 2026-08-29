using Microsoft.Extensions.DependencyInjection;

namespace BOS.Modules.Abstractions;

/// <summary>
/// Represents a pluggable business module.
/// </summary>
public interface IBosModule
{
    string Name { get; }
    string Description { get; }
    string Version { get; }

    /// <summary>
    /// Registers module services into the DI container.
    /// </summary>
    IServiceCollection RegisterServices(IServiceCollection services);
}
