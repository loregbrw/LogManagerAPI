namespace Application.Models.Responses.StockItem;

public record StockResponse(
    long OutOfStock,
    long LowStock,
    decimal StockValue
);