namespace API.Features.StockItem.Post;

using Application.Exceptions;
using Application.Interfaces.Services.Domain;
using Application.Models.Responses.Import;

public class ImportStockItemsHandler(IStockItemService service)
{
    private readonly IStockItemService _service = service;

    public async Task<ImportCsvResponse> HandleAsync(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new BadRequestException("MissingCsv");

        return await _service.ImportFromCsvAsync(file.OpenReadStream());
    }
}