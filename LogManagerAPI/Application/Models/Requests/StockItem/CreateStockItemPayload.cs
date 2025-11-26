namespace Application.Models.Requests.StockItem;

using System.ComponentModel.DataAnnotations;
using Application.Enums;

public class CreateStockItemPayload
{
    [StringLength(50)]
    public required string Code { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public Guid? UnitOfMeasurementId { get; set; }

    [StringLength(255)]
    public string? Localization { get; set; }

    public Guid? StockDepartmentId { get; set; }

    [EnumDataType(typeof(EStockGroup))]
    public EStockGroup? StockGroup { get; set; }

    public Guid? StockSubgroupId { get; set; }

    public decimal? Cost { get; set; }

    public short? MinimumStock { get; set; }
}