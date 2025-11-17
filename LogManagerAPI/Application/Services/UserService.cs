namespace Application.Services;

using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using Application.Enums;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Core;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Pagination;
using Application.Models.Requests.User;
using Application.Models.Responses.Csv;
using Application.Models.Responses.Enum;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class UserService(
    IUserRepository repository, IUserDepartmentRepository userDepartmentRepository,
    IUserMapper mapper, ICsvService csvService, IDateTimeProvider dateTimeProvider, IEnumHelper enumHelper
) : BaseService<User, UserDto>(repository, mapper), IUserService
{
    private readonly IUserRepository _repo = repository;
    private readonly IUserMapper _mapper = mapper;
    private readonly ICsvService _csvService = csvService;
    private readonly IUserDepartmentRepository _userDepartmentRepo = userDepartmentRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IEnumHelper _enumHelper = enumHelper;

    public async Task<PaginatedResult<UserDto>> GetPaginatedUsersAsync(int page, int size, string? search = null)
    {
        var query = _repo.GetAllAsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(u => EF.Functions.ILike(u.Name, $"%{search}%"));

        return await query
            .Include(u => u.UserDepartment)
            .OrderBy(u => u.Name)
            .ToPaginatedResultAsync(_mapper, page, size);
    }

    public GetValuesResponse GetUserRoles()
    {
       var values = _enumHelper.GetEnumValuesResponse<ERole>();
       return new GetValuesResponse(values);
    }

    public async Task<ImportCsvResponse> ImportFromCsvAsync(Stream fileStream)
    {
        var records = _csvService.ImportFromCsv<UserCsv>(fileStream);
        var departments = await _userDepartmentRepo.GetAll().ToDictionaryAsync(d => d.Name, d => d);

        var existingCodes = (await _repo.GetAll().Select(u => u.Code).ToListAsync()).ToHashSet();
        var existingEmails = (await _repo.GetAll().Where(u => u.Email != null).Select(u => u.Email).ToListAsync()).ToHashSet();

        int ImportedItems = 0;

        foreach (var record in records)
        {
            if (!record.Code.HasValue || string.IsNullOrWhiteSpace(record.Name))
                continue;

            if (existingCodes.Contains(record.Code.Value))
                throw new ConflictException("UserCodeAlreadyExists", record.Code);

            if (record.Email is not null && existingEmails.Contains(record.Email))
                throw new ConflictException("UserEmailAlreadyExists", record.Email);

            var user = _mapper.FromUserCsv(record);

            if (!string.IsNullOrWhiteSpace(record.Department))
            {
                if (!departments.TryGetValue(record.Department, out var department))
                {
                    department = new UserDepartment { Name = record.Department };
                    departments.Add(record.Department, department);
                }
                user.UserDepartment = department;
            }

            await _repo.AddAsync(user);
            ImportedItems++;

            existingCodes.Add(user.Code);
            if (user.Email is not null) existingEmails.Add(user.Email);
        }

        await _repo.SaveChangesAsync();

        return new ImportCsvResponse(
            ImportedItems
        );
    }

    public async Task<ExportCsvResponse> ExportToCsvAsync(char? delimiter)
    {
        var users = await _repo.GetAllAsNoTracking()
            .Include(s => s.UserDepartment)
            .OrderBy(s => s.Code)
            .ToListAsync();

        var stream = _csvService.ExportToCsv(users.Select(_mapper.ToUserCsv), delimiter ?? ';');
        var fileName = $"users-{_dateTimeProvider.UtcNow:dd-MM-yyyy-HH-mm-ss}.csv";

        return new ExportCsvResponse(stream, fileName, "text/csv");
    }
}