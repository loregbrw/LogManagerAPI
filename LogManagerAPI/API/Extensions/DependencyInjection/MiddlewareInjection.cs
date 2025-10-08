namespace API.Extensions.DependencyInjection;

using API.Middlewares;

public static class MiddlewareInjection
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationMiddleware>();

        return services;
    }
}