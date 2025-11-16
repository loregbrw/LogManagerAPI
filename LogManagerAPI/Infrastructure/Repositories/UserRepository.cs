namespace Infrastructure.Repositories;

using Application.Entities;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Primitives;
using Microsoft.EntityFrameworkCore;

public class UserRepository(
    LogManagerDbContext context
) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsNoTrackingAsync(string email, CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<User?> GetByCodeAsNoTrackingAsync(short code, CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().SingleOrDefaultAsync(u => u.Code == code, cancellationToken);
}