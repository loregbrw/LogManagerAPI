namespace API.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for registering the main configurations and services required by the application.
/// </summary>
public static class MainConfigsInjection
{
    /// <summary>
    /// Adds core services and configurations to the application's dependency injection container,
    /// including CORS, controllers, authorization, problem details, API explorer, and Swagger.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the main configurations registered.</returns>
    public static IServiceCollection AddMainConfigs(this IServiceCollection services)
    {
        services.AddCors();

        services.AddControllers();

        services.AddAuthorization();
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}