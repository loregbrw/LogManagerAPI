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
    public async Task<User?> GetByEmailAsNoTrackingAsync(string email, CancellationToken cancellationToken = default) =>
    await _dbSet.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
}