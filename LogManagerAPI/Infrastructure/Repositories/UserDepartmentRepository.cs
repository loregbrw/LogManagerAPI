namespace Infrastructure.Repositories;

using Application.Entities;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Primitives;

public class UserDepartmentRepository(
    LogManagerDbContext context, IDateTimeProvider dateTimeProvider
) : BaseRepository<UserDepartment>(context, dateTimeProvider), IUserDepartmentRepository;