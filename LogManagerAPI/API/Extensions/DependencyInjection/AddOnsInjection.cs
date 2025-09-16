namespace API.Extensions.DependencyInjection;

using Application.Interfaces.Providers;
using Application.Interfaces.Services.Core;
using Infrastructure.Providers;
using Infrastructure.Services;

/// <summary>
/// Provides extension methods for injecting additional services into the application's dependency injection container.
/// </summary>
public static class AddOnsInjection
{
    /// <summary>
    /// Registers additional services (such as user context, password hasher, and date/time provider)
    /// into the dependency injection container.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the services will be added.</param>
    /// <returns>The updated IServiceCollection with the added services.</returns>
    public static IServiceCollection AddAddOns(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}