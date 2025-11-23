namespace API.Extensions.DependencyInjection;

using API.Features.StockItem.Get;
using API.Features.StockItem.Post;
using API.Features.User.Get;
using API.Features.User.Patch;
using API.Features.User.Post;
using Application.Interfaces.Services.Core;
using Application.Interfaces.Services.Core.Auth;
using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Services;
using Application.Services.Primitives;
using Infrastructure.Services;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<GetLoggedUserHandler>();
        services.AddScoped<GetPaginatedUsersHandler>();
        services.AddScoped<ImportUsersHandler>();
        services.AddScoped<UpdateUserHandler>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<GetPaginatedStockItemsHandler>();
        services.AddScoped<ImportStockItemsHandler>();
        services.AddScoped<IStockItemService, StockItemService>();

        services.AddScoped<IUserDepartmentService, UserDepartmentService>();

        return services;
    }
}