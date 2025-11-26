namespace API.Extensions.DependencyInjection;

using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Mappers;
using Application.Models.Entities;

public static class MappersInjection
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IRegisterMapper, RegisterMapper>();
        services.AddScoped<IStockDepartmentMapper, StockDepartmentMapper>();
        services.AddScoped<IStockItemMapper, StockItemMapper>();
        services.AddScoped<IStockSubgroupMapper, StockSubgroupMapper>();
        services.AddScoped<IUnitOfMeasurementMapper, UnitOfMeasurementMapper>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IUserDepartmentMapper, UserDepartmentMapper>();

        return services;
    }
}