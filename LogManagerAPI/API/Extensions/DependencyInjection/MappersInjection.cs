namespace API.Extensions.DependencyInjection;

using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Mappers;
using Application.Models.Entities;

public static class MappersInjection
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IStockItemMapper, StockItemMapper>();
        services.AddScoped<IUnitOfMeasurementMapper>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IUserDepartmentMapper, UserDepartmentMapper>();

        return services;
    }
}