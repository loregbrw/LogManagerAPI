namespace API.Features.User.Post;

using Application.Exceptions;
using Application.Interfaces.Services.Domain;

public class ImportUsersHandler(IUserService service)
{
    private readonly IUserService _service = service;

    public async Task HandleAsync(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new BadRequestException("MissingCsv");

        await _service.ImportFromCsvAsync(file.OpenReadStream());
    }
}