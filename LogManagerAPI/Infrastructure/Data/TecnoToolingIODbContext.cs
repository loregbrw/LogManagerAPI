namespace Infrastructure.Data;

using Application.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class LogManagerDbContext(DbContextOptions<LogManagerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}