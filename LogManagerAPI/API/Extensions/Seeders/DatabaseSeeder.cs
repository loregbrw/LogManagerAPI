namespace API.Extensions.Seeders;

using Application.Entities;
using Application.Enums;
using Application.Interfaces.Providers;
using Application.Interfaces.Services.Core.Auth;
using Application.Models.Options;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class DatabaseSeeder
{
    public static async Task SeedAdminUserAsync(this LogManagerDbContext context, IPasswordHasher hasher, IDateTimeProvider dateTimeProvider, IOptions<AdminUserOptions> adminOptions)
    {
        if (!await context.Users.AnyAsync())
        {
            var options = adminOptions.Value;

            var admin = new User()
            {
                Code = options.Code,
                Name = options.Name,
                Email = options.Email,
                Password = hasher.Hash(options.Password),
                Role = Enum.Parse<ERole>(options.Role, true),
                CreatedAt = dateTimeProvider.UtcNow
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}