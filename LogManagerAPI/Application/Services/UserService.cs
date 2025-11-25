namespace Application.Services;

using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Entities;
using Application.Enums;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Core;
using Application.Interfaces.Services.Core.Auth;
using Application.Interfaces.Services.Domain;
using Application.Models;
using Application.Models.Entities;
using Application.Models.Pagination;
using Application.Models.Requests.User;
using Application.Models.Responses.Csv;
using Application.Models.Responses.Value;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public partial class UserService(
    IUserRepository repository, IUserDepartmentRepository userDepartmentRepository,
    IUserMapper mapper, ICsvService csvService, IDateTimeProvider dateTimeProvider, IEnumHelper enumHelper,
    IJwtService jwtService, IEmailSenderService emailSenderService, IEmailTemplateHelper emailTemplateHelper, IPasswordHasher hasher
) : BaseService<User, UserDto>(repository, mapper), IUserService
{

    [GeneratedRegex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*_\-])[A-Za-z\d!@#$%^&*_\-]{8,}$")]
    private static partial Regex PasswordRegex();
    private readonly IUserRepository _repo = repository;
    private readonly IUserMapper _mapper = mapper;
    private readonly ICsvService _csvService = csvService;
    private readonly IUserDepartmentRepository _userDepartmentRepo = userDepartmentRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IEnumHelper _enumHelper = enumHelper;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IEmailSenderService _emailSenderService = emailSenderService;
    private readonly IEmailTemplateHelper _emailTemplateHelper = emailTemplateHelper;
    private readonly IPasswordHasher _hasher = hasher;

    public async Task<UserDto> CreateUserAsync(CreateUserPayload payload)
    {
        if (await _repo.GetByCodeAsNoTrackingAsync(payload.Code) is not null)
            throw new ConflictException("UserCodeAlreadyExists", payload.Code);

        if (payload.Email is not null && await _repo.GetAllAsNoTracking().AnyAsync(u => u.Email == payload.Email))
            throw new ConflictException("UserEmailAlreadyExists", payload.Email);

        var user = new User
        {
            Code = payload.Code,
            Name = payload.Name,
            Email = payload.Email
        };

        if (payload.UserDepartmentId.HasValue)
        {
            var department = await _userDepartmentRepo.GetByIdAsync(payload.UserDepartmentId.Value)
                ?? throw new NotFoundException("EntityNotFound", typeof(UserDepartment));

            user.UserDepartment = department;
        }

        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();

        return _mapper.ToDto(user);
    }

    public async Task RegisterUserAsync(RegisterNewUserPayload payload)
    {
        var user = await _repo.GetByIdAsNoTrackingAsync(payload.UserId)
            ?? throw new NotFoundException("EntityNotFound", typeof(User));

        if (user.Email is null)
            throw new BadRequestException("UserMissingEmail");

        var jwt = _jwtService.GenerateToken(payload.UserId, payload.UserRole);
        _emailSenderService.SendEmail(user.Email, _emailTemplateHelper.GetSubject(), _emailTemplateHelper.GetRegistrationEmail(jwt));
    }

    public async Task<UserDto> GetRegisteringUserAsync(string token)
    {
        var (user, _) = await GetRegisteringUserAndContextData(token);
        return _mapper.ToDto(user);
    }

    public async Task<UserDto> CompleteUserRegistrationAsync(string token, RegisterUserPasswordPayload payload)
    {
        var (user, contextData) = await GetRegisteringUserAndContextData(token);

        if (!PasswordRegex().IsMatch(payload.UserPassword))
            throw new BadRequestException("InvalidPassword");

        user.Password = _hasher.Hash(payload.UserPassword);
        user.Role = contextData.UserRole;

        _repo.Update(user);
        await _repo.SaveChangesAsync();

        return _mapper.ToDto(user);
    }

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

    private async Task<(User, ContextData)> GetRegisteringUserAndContextData(string token)
    {
        var contextData = _jwtService.ValidateToken(token);

        var user = await _repo.GetByIdAsync(contextData.UserId)
            ?? throw new NotFoundException("EntityNotFound", typeof(User));

        if (user.Email is null)
            throw new ConflictException("MissingRegistrationEmail");

        if (user.Password is not null && user.Role != ERole.DATA)
            throw new ConflictException("UserAlreadyRegistered", user.Email);

        return (user, contextData);
    }

    public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserPayload payload)
    {
        var user = await _repo.GetByIdAsync(userId)
            ?? throw new NotFoundException("EntityNotFound", typeof(User));

        if (payload.Code.HasValue && payload.Code.Value != user.Code)
        {
            if (await _repo.GetByCodeAsNoTrackingAsync(payload.Code.Value) is not null)
                throw new ConflictException("UserCodeAlreadyExists", payload.Code.Value);

            user.Code = payload.Code.Value;
        }

        if (payload.Email is not null && payload.Email != user.Email)
        {
            if (await _repo.GetAllAsNoTracking().AnyAsync(u => u.Email == payload.Email))
                throw new ConflictException("UserEmailAlreadyExists", payload.Email);

            user.Email = payload.Email;
        }

        if (payload.Name is not null) user.Name = payload.Name;

        if (payload.UserDepartmentId.HasValue)
        {
            var department = await _userDepartmentRepo.GetByIdAsync(payload.UserDepartmentId.Value)
                ?? throw new NotFoundException("EntityNotFound", typeof(UserDepartment));

            user.UserDepartment = department;
        }

        if (payload.UserRole.HasValue) user.Role = payload.UserRole.Value;

        if (payload.Password is not null)
        {
            if (!PasswordRegex().IsMatch(payload.Password))
                throw new BadRequestException("InvalidPassword");

            user.Password = _hasher.Hash(payload.Password);
        }

        _repo.Update(user);
        await _repo.SaveChangesAsync();

        return _mapper.ToDto(user);
    }
}