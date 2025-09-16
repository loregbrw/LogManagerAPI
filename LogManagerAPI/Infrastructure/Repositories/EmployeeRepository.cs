namespace Infrastructure.Repositories;

using Application.Entities;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Primitives;
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository(
    TecnoToolingIODbContext context, IDateTimeProvider dateTimeProvider
) : BaseRepository<Employee>(context, dateTimeProvider), IEmployeeRepository
{
}