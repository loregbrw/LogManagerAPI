namespace API.Features.StockItem;

using API.Features.StockItem.Get;
using API.Features.StockItem.Post;
using Application.Enums;
using Application.Interfaces.Services.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock-items")]
public class StockItemController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetStockItems(
        [FromServices] IStockItemService service
    )
    {
        var result = await service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedStockItems(
        [FromServices] GetPaginatedStockItemsHandler handler,
        [FromQuery] string? query, [FromQuery] int? page, [FromQuery] int? count,
        [FromQuery] EStockGroup? group, [FromQuery] EStockItemStatus? status
    )
    {
        var result = await handler.HandleAsync(query, page, count, group, status);
        return Ok(result);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportStockItems(
        [FromServices] ImportStockItemsHandler handler, [FromForm] IFormFile file
    )
    {
        var result = await handler.HandleAsync(file);
        return Ok(result);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportStockItems(
        [FromServices] IStockItemService service, [FromQuery] char? delimiter
    )
    {
        var result = await service.ExportToCsvAsync(delimiter);
        return File(result.Content, result.ContentType, result.FileName);
    }
}