namespace API.Extensions.DependencyInjection;

using API.Features.User.Get;
using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Primitives;
using Application.Services;
using Application.Services.Primitives;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

        services.AddScoped<GetPaginatedUsersHandler>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}