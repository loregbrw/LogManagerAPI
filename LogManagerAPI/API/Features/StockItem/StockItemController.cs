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
    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedStockItems(
        [FromServices] GetPaginatedStockItemsHandler handler,
        [FromQuery] string? query, [FromQuery] int? page, [FromQuery] int? count,
        [FromQuery] EStockGroup? group, [FromQuery] EStockItemStatus? status
    )
    {
        var response = await handler.HandleAsync(query, page, count, group, status);
        return Ok(response);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportStockItems(
        [FromServices] ImportStockItemsHandler handler, [FromForm] IFormFile file
    )
    {
        var response = await handler.HandleAsync(file);
        return Ok(response);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportStockItems(
        [FromServices] IStockItemService service, [FromQuery] char? delimiter
    )
    {
        var response = await service.ExportToCsvAsync(delimiter);
        return File(response.Content, response.ContentType, response.FileName);
    }
}