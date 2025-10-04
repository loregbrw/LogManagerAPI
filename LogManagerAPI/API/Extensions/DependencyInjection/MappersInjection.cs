namespace API.Extensions.DependencyInjection;

using Application.Entities;
using Application.Mappers;
using Application.Mappers.Primitives;
using Application.Models.Entities;

public static class MappersInjection
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IEntityMapper<User, UserDto>, UserMapper>();

        return services;
    }
}