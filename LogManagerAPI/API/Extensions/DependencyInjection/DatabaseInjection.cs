namespace API.Extensions.DependencyInjection;

using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Provides extension methods for registering the application's database context and related services.
/// </summary>
public static class DatabaseInjection
{
    /// <summary>
    /// Registers the application's <see cref="LogManagerDbContext"/> with the dependency injection container,
    /// using a PostgreSQL connection string from the configuration.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the database context will be added.
    /// </param>
    /// <param name="configuration">
    /// The application configuration that contains the connection string named "Postgres".
    /// </param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance, enabling fluent chaining.
    /// </returns>
    /// <remarks>
    /// The database context is configured to use Npgsql (PostgreSQL). 
    /// Make sure the "Postgres" connection string exists in your configuration files.
    /// </remarks>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<LogManagerDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}