namespace Application.Models.Requests.StockItem;

using System.ComponentModel;
using Application.Attributes;
using Application.Converters;
using Application.Enums;

public class StockItemCsv
{
    [AppStringLength(50)]
    [AppAlias("CÓDIGO - GRV")]
    public string? Code { get; set; }

    [AppStringLength(500)]
    [AppAlias("DESCRIÇÃO")]
    public string? Description { get; set; }

    [AppStringLength(50)]
    [AppAlias("UNI.MEDIDA")]
    public string? UnitOfMeasurement { get; set; }

    [AppStringLength(255)]
    [AppAlias("LOCALIZAÇÃO/CAIXA")]
    public string? Localization { get; set; }

    [AppStringLength(100)]
    [AppAlias("SETOR")]
    public string? Department { get; set; }

    [AppAlias("GRUPO")]
    [AppConverter(typeof(AppEnumFlexibleConverter<EStockGroup>))]
    public EStockGroup? Group { get; set; }

    [AppStringLength(255)]
    [AppAlias("SUBGRUPO")]
    public string? Subgroup { get; set; }

    [AppAlias("CUSTO")]
    [AppConverter(typeof(AppCurrencyConverter))]
    public decimal? Cost { get; set; }

    [AppAlias("EST.MINIMO")]
    public short? MinimumStock { get; set; }

    [AppAlias("ENTRADAS")]
    public long Inbound { get; set; } = 0;

    [AppAlias("SAÍDA")]
    public long Outbound { get; set; } = 0;

    [AppAlias("ATUAL")]
    public long Current { get; set; } = 0;

    [AppAlias("SITUAÇÃO")]
    [AppConverter(typeof(AppEnumFlexibleConverter<EStockSituation>))]
    public EStockSituation? Situation { get; set; }
}