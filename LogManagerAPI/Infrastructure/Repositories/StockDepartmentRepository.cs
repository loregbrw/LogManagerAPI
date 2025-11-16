namespace Infrastructure.Repositories;

using Application.Entities;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Primitives;

public class StockDepartmentRepository(
    LogManagerDbContext context
) : BaseRepository<StockDepartment>(context), IStockDepartmentRepository;