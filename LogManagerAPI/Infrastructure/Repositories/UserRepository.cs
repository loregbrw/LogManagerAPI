namespace Infrastructure.Repositories;

using Application.Entities;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Primitives;
using Microsoft.EntityFrameworkCore;

public class UserRepository(
    LogManagerDbContext context, IDateTimeProvider dateTimeProvider
) : BaseRepository<User>(context, dateTimeProvider), IUserRepository
{
}