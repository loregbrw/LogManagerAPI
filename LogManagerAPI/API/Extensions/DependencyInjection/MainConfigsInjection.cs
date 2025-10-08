namespace API.Extensions.DependencyInjection;

using Newtonsoft.Json.Converters;

public static class MainConfigsInjection
{
    public static IServiceCollection AddMainConfigs(this IServiceCollection services)
    {
        services.AddCors();

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        services.AddAuthorization();
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}