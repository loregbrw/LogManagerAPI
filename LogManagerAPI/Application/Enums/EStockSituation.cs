using Application.Attributes;

namespace Application.Enums;

public enum EStockSituation
{
    OK,

    [AppAlias("COMPRAR")]
    TOBUY
}