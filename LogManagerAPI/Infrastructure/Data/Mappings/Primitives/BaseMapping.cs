namespace Infrastructure.Data.Mappings.Primitives;

using Application.Entities.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class BaseMapping<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id)
               .HasName($"PK_{typeof(T).Name}");

        builder.Property(e => e.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("gen_random_uuid()")
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