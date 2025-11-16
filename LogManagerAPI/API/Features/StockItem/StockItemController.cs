namespace API.Features.StockItem;

using API.Features.StockItem.Get;
using API.Features.StockItem.Post;
using Application.Enums;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stockItems")]
public class StockItemController : ControllerBase
{
    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedUsers(
        [FromServices] GetPaginatedStockItemsHandler handler,
        [FromQuery] string? query, [FromQuery] int? page, [FromQuery] int? count,
        [FromQuery] EStockGroup? group, [FromQuery] EStockItemStatus? status
    )
    {
        var response = await handler.HandleAsync(query, page, count, group, status);
        return Ok(response);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportUsers(
        [FromServices] ImportStockItemsHandler handler, [FromForm] IFormFile file
    )
    {
        var response = await handler.HandleAsync(file);
        return Ok(response);
    }
}