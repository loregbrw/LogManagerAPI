namespace API.Features.StockItem;

using API.Attributes;
using API.Features.StockItem.Get;
using API.Features.StockItem.Post;
using Application.Enums;
using Application.Interfaces.Services.Domain;
using Application.Models.Requests.StockItem;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock-items")]
public class StockItemController : ControllerBase
{
    [HttpGet("values")]
    public async Task<IActionResult> GetStockItemValues(
        [FromServices] IStockItemService service
    )
    {
        var result = await service.GetStockItemValuesAsync();
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

    [HttpPost]
    public async Task<IActionResult> CreateStockItem(
        [FromServices] IStockItemService service, [FromBody] CreateStockItemPayload payload
    )
    {
        var result = service.CreateStockItemAsync(payload);
        return Ok(result);
    }

    [HttpPost("import")]
    [ManagerAuthentication]
    public async Task<IActionResult> ImportStockItems(
        [FromServices] ImportStockItemsHandler handler, [FromForm] IFormFile file
    )
    {
        var result = await handler.HandleAsync(file);
        return Ok(result);
    }

    [HttpGet("export")]
    [ManagerAuthentication]
    public async Task<IActionResult> ExportStockItems(
        [FromServices] IStockItemService service, [FromQuery] char? delimiter
    )
    {
        var result = await service.ExportToCsvAsync(delimiter);
        return File(result.Content, result.ContentType, result.FileName);
    }
}