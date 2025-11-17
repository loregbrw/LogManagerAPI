namespace Application.Services;

using Application.Entities;
using Application.Enums;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Core;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Requests.StockItem;
using Application.Models.Responses.Csv;
using Application.Models.Responses.StockItem;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class StockItemService(
    IStockItemRepository repository, IUnitOfMeasurementRepository unitOfMeasurementRepository, IStockDepartmentRepository stockDepartmentRepository, IStockSubgroupRepository stockSubgroupRepository,
    IStockItemMapper mapper, ICsvService csvService, IDateTimeProvider dateTimeProvider
) : BaseService<StockItem, StockItemDto>(repository, mapper), IStockItemService
{
    private readonly IStockItemRepository _repo = repository;
    private readonly IStockItemMapper _mapper = mapper;
    private readonly ICsvService _csvService = csvService;
    private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository = unitOfMeasurementRepository;
    private readonly IStockDepartmentRepository _stockDepartmentRepository = stockDepartmentRepository;
    private readonly IStockSubgroupRepository _stockSubgroupRepository = stockSubgroupRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

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
                EF.Functions.ILike(s.Code, $"%{search}%") ||
                EF.Functions.ILike(s.Description!, $"%{search}%"));

        if (stockGroup is not null)
            query = query.Where(s => s.StockGroup == stockGroup);

        if (status is not null)
            query = query.Where(s =>
                (status == EStockItemStatus.OUTOFSTOCK && s.Current == 0) ||
                (status == EStockItemStatus.LOWSTOCK && s.Current < (s.MinimumStock ?? 0) && s.Current > 0) ||
                (status == EStockItemStatus.INSTOCK && s.Current >= (s.MinimumStock ?? 0))
            );

        var paginatedResult = await query
            .Include(s => s.UnitOfMeasurement)
            .OrderBy(s => s.Code)
            .ToPaginatedResultAsync(_mapper, page, size);

        return new PaginatedStockItemResponse(
            Overview: overview,
            PaginatedItems: paginatedResult.PaginatedItems,
            Pagination: paginatedResult.Pagination
        );
    }

    public async Task<ImportCsvResponse> ImportFromCsvAsync(Stream fileStream)
    {
        var records = _csvService.ImportFromCsv<StockItemCsv>(fileStream);
        var unitsOfMeasurement = await _unitOfMeasurementRepository.GetAll().ToDictionaryAsync(u => u.Name, d => d);
        var stockDepartments = await _stockDepartmentRepository.GetAll().ToDictionaryAsync(s => s.Name, d => d);
        var stockSubgroups = await _stockSubgroupRepository.GetAll().ToDictionaryAsync(s => s.Name, d => d);

        var existingCodes = (await _repo.GetAll().Select(s => s.Code).ToListAsync()).ToHashSet();

        int ImportedItems = 0;

        foreach (var record in records)
        {
            if (string.IsNullOrWhiteSpace(record.Code) || existingCodes.Contains(record.Code))
                continue;

            var stockItem = _mapper.FromStockItemCsv(record);

            if (!string.IsNullOrWhiteSpace(record.UnitOfMeasurement))
            {
                if (!unitsOfMeasurement.TryGetValue(record.UnitOfMeasurement, out var unitOfMeasurement))
                {
                    unitOfMeasurement = new UnitOfMeasurement { Name = record.UnitOfMeasurement };
                    unitsOfMeasurement.Add(record.UnitOfMeasurement, unitOfMeasurement);
                }
                stockItem.UnitOfMeasurement = unitOfMeasurement;
            }

            if (!string.IsNullOrWhiteSpace(record.Department))
            {
                if (!stockDepartments.TryGetValue(record.Department, out var department))
                {
                    department = new StockDepartment { Name = record.Department };
                    stockDepartments.Add(record.Department, department);
                }
                stockItem.StockDepartment = department;
            }

            if (!string.IsNullOrWhiteSpace(record.Subgroup))
            {
                if (!stockSubgroups.TryGetValue(record.Subgroup, out var subgroup))
                {
                    subgroup = new StockSubgroup { Name = record.Subgroup };
                    stockSubgroups.Add(record.Subgroup, subgroup);
                }
                stockItem.StockSubgroup = subgroup;
            }

            await _repo.AddAsync(stockItem);
            ImportedItems++;

            existingCodes.Add(stockItem.Code);
        }

        await _repo.SaveChangesAsync();

        return new ImportCsvResponse(
            ImportedItems
        );
    }

    public async Task<ExportCsvResponse> ExportToCsvAsync(char? delimiter)
    {
        var stockItems = await _repo.GetAllAsNoTracking()
            .Include(s => s.UnitOfMeasurement)
            .Include(s => s.StockDepartment)
            .Include(s => s.StockSubgroup)
            .OrderBy(s => s.Code)
            .ToListAsync();

        var stream = _csvService.ExportToCsv(stockItems.Select(_mapper.ToStockItemCsv), delimiter ?? ';');
        var fileName = $"stock-items-{_dateTimeProvider.UtcNow:dd-MM-yyyy-HH-mm-ss}.csv";

        return new ExportCsvResponse(stream, fileName, "text/csv");
    }
}
