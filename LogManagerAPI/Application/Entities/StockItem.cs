namespace Application.Entities;

using Application.Entities.Primitives;
using Application.Enums;

public class StockItem : BaseEntity
{
    public required string Code { get; set; }
    public string? Description { get; set; }
    public UnitOfMeasurement? UnitOfMeasurement { get; set; }
    public string? Localization { get; set; }
    public StockDepartment? StockDepartment { get; set; }
    public EStockGroup? StockGroup { get; set; }
    public StockSubgroup? StockSubgroup { get; set; }
    public decimal? Cost { get; set; }
    public short? MinimumStock { get; set; }
    public int Inbound { get; set; } = 0;
    public int Outbound { get; set; } = 0;
    public int Current { get; set; } = 0;
    public EStockSituation? StockSituation { get; set; }
}