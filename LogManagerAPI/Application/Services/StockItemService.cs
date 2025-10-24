namespace Application.Services;

using Application.Entities;
using Application.Enums;
using Application.Extensions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Mappers.Primitives;
using Application.Models.Entities;
using Application.Models.Responses.StockItem;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class StockItemService(
    IStockItemRepository repository,
    IStockItemMapper mapper
) : BaseService<StockItem, StockItemDto>(repository, mapper), IStockItemService
{
    private readonly IStockItemRepository _repo = repository;
    private readonly IStockItemMapper _mapper = mapper;

    public async Task<PaginatedStockItemResponse> GetPaginatedStockItemsAsync(
        int page,
        int size,
        string? search = null,
        EStockGroup? stockGroup = null,
        EStockItemStatus? status = null
    )
    {
        var query = _repo.GetAllAsNoTracking();

        var overview = await query
            .GroupBy(_ => 1)
            .Select(g => new StockResponse(
                g.Count(s => s.Current == 0),
                g.Count(s => s.Current < (s.MinimumStock ?? 0) && s.Current > 0),
                g.Sum(s => (s.Cost ?? 0) * s.Current)
            ))
            .FirstOrDefaultAsync() ?? new StockResponse(0, 0, 0);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s =>
                EF.Functions.Like(s.Code, $"%{search}%") ||
                EF.Functions.Like(s.Description!, $"%{search}%"));

        if (stockGroup is not null)
            query = query.Where(s => s.StockGroup == stockGroup);

        if (status is not null)
            query = query.Where(s =>
                (status == EStockItemStatus.OUTOFSTOCK && s.Current == 0) ||
                (status == EStockItemStatus.LOWSTOCK && s.Current < (s.MinimumStock ?? 0) && s.Current > 0) ||
                (status == EStockItemStatus.INSTOCK && s.Current >= (s.MinimumStock ?? 0))
            );

        var paginatedResult = await query
            .OrderBy(s => s.Code)
            .ToPaginatedResultAsync(_mapper, page, size);

        return new PaginatedStockItemResponse(
            Overview: overview,
            PaginatedItems: paginatedResult.PaginatedItems,
            Pagination: paginatedResult.Pagination
        );
    }
}
