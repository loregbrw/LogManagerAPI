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
    /// Registers additional application services in the dependency injection container.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the services will be added.
    /// </param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance, enabling fluent chaining.
    /// </returns>
    /// <remarks>
    /// The following services are registered:
    /// <list type="bullet">
    ///   <item><description><see cref="IPasswordHasher"/> → Scoped lifetime, implemented by <see cref="PasswordHasher"/>.</description></item>
    ///   <item><description><see cref="IDateTimeProvider"/> → Singleton lifetime, implemented by <see cref="SystemDateTimeProvider"/>.</description></item>
    /// </list>
    /// </remarks>
    public static IServiceCollection AddAddOns(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}