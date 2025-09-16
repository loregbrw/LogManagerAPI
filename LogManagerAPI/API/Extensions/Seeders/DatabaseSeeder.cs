namespace API.Extensions.Seeders;

using Application.Entities;
using Application.Enums;
using Application.Interfaces.Services.Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public static class DatabaseSeeder
{
    public static async Task SeedAdminEmployeeAsync(this LogManagerDbContext context, IPasswordHasher hasher)
    {
        if (!await context.Employees.AnyAsync())
        {
            var admin = new Employee()
            {
                Code = 0,
                Name = "Admin",
                Email = "admin@logmanager.com",
                Password = hasher.Hash("Admin123!"),
                Role = ERole.ADMIN
            };

            context.Employees.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}