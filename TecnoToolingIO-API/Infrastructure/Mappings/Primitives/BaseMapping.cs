namespace Infrastructure.Mappings.Primitives;

using Application.Entities.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Provides a base entity mapping configuration for entities inheriting from <see cref="BaseEntity"/>.
/// This configuration sets up primary key, column names, and a global query filter for soft deletes.
/// </summary>
/// <typeparam name="T">The entity type inheriting from <see cref="BaseEntity"/>.</typeparam>
public abstract class BaseMapping<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    /// <summary>
    /// Configures the entity of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="builder">The builder to configure the entity type.</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id)
               .HasName($"PK_{typeof(T).Name}");

        builder.Property(e => e.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at");

        builder.Property(e => e.UpdatedAt)
               .HasColumnName("updated_at");

        builder.Property(e => e.DeletedAt)
               .HasColumnName("deleted_at");

        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}