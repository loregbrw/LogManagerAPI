namespace Application.Interfaces.Repositories;

using Application.Entities;
using Application.Interfaces.Repositories.Primitives;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsNoTrackingAsync(string email, CancellationToken cancellationToken = default);
}