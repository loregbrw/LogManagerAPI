using Application.Attributes;

namespace Application.Enums;

public enum EStockGroup
{
    [AppAlias("DIRETOS")]
    DIRECT,

    [AppAlias("INDIRETOS")]
    INDIRECT,

    [AppAlias("CONSUMO")]
    CONSUMPTION
}