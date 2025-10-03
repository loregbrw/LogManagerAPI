namespace API.Extensions.DependencyInjection;

using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public static class DatabaseInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<LogManagerDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}