namespace API.Extensions.DependencyInjection;

using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Primitives;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Primitives;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IStockDepartmentRepository, StockDepartmentRepository>();
        services.AddScoped<IStockItemRepository, StockItemRepository>();
        services.AddScoped<IStockSubgroupRepository, StockSubgroupRepository>();
        services.AddScoped<IUnitOfMeasurementRepository, UnitOfMeasurementRepository>();
        services.AddScoped<IUserDepartmentRepository, UserDepartmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}