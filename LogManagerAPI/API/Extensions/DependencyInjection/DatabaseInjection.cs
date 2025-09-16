namespace API.Extensions.DependencyInjection;

using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Provides extension methods for registering the application's database context and related services.
/// </summary>
public static class DatabaseInjection
{
    /// <summary>
    /// Registers the <see cref="LogManagerDbContext"/> using the PostgreSQL provider with the connection string
    /// from the configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the database context will be added.</param>
    /// <param name="configuration">The ConfigurationManager used to retrieve the connection string.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the database context registered.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<LogManagerDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}