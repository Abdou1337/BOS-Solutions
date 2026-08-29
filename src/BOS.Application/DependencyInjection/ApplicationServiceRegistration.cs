using Microsoft.Extensions.DependencyInjection;

namespace BOS.Application.DependencyInjection;

/// <summary>
/// Registers application layer services.
/// </summary>
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Future: register command/query handlers, validators, etc.
        return services;
    }
}
