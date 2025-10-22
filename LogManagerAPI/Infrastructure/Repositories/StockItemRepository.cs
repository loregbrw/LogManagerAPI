namespace Infrastructure.Repositories;

using Application.Entities;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Primitives;

public class StockItemRepository(
    LogManagerDbContext context, IDateTimeProvider dateTimeProvider
) : BaseRepository<StockItem>(context, dateTimeProvider), IStockItemRepository
{ }