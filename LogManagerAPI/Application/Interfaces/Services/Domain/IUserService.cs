namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Pagination;
using Application.Models.Requests.User;
using Application.Models.Responses.Csv;
using Application.Models.Responses.Enum;

public interface IUserService : IBaseService<User, UserDto>
{
    Task<UserDto> CreateUserAsync(CreateUserPayload payload);
    Task RegisterUserAsync(RegisterNewUserPayload payload);
    Task<UserDto> GetRegisteringUserAsync(string token);
    Task<UserDto> CompleteUserRegistrationAsync(string token, RegisterUserPasswordPayload payload);
    Task<PaginatedResult<UserDto>> GetPaginatedUsersAsync(int page, int size, string? search = null);
    Task<ImportCsvResponse> ImportFromCsvAsync(Stream fileStream);
    Task<ExportCsvResponse> ExportToCsvAsync(char? delimiter);
    GetValuesResponse GetUserRoles();
}