namespace Infrastructure.Data;

using Application.Entities;
using Application.Entities.Primitives;
using Application.Interfaces.Providers;
using Microsoft.EntityFrameworkCore;

public sealed class LogManagerDbContext(DbContextOptions<LogManagerDbContext> options, IDateTimeProvider dateTimeProvider) : DbContext(options)
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditingRules();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditingRules()
    {
        var now = _dateTimeProvider.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedAt = now;
                    break;
            }
        }
    }
}