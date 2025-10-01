namespace API.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for registering the main configurations and services required by the application.
/// </summary>
public static class MainConfigsInjection
{
    /// <summary>
    /// Adds the core application services and configurations to the dependency injection container.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the main configurations and services will be added.
    /// </param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance, enabling fluent chaining.
    /// </returns>
    /// <remarks>
    /// This method registers the following services:
    /// <list type="bullet">
    ///   <item><description>CORS support via AddCors extension method.</description></item>
    ///   <item><description>Controllers using AddControllers extension method.</description></item>
    ///   <item><description>Authorization services using AddAuthorization extension method.</description></item>
    ///   <item><description>Problem details middleware via AddProblemDetails extension method.</description></item>
    ///   <item><description>API endpoints explorer via AddEndpointsApiExplorer extension method.</description></item>
    ///   <item><description>Swagger generation via AddSwaggerGen extension method.</description></item>
    /// </list>
    /// </remarks>
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