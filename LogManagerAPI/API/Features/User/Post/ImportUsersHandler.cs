namespace API.Features.User.Post;

using Application.Exceptions;
using Application.Interfaces.Services.Domain;
using Application.Models.Responses.Csv;

public class ImportUsersHandler(IUserService service)
{
    private readonly IUserService _service = service;

    public async Task<ImportCsvResponse> HandleAsync(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new BadRequestException("MissingCsv");

        return await _service.ImportFromCsvAsync(file.OpenReadStream());
    }
}