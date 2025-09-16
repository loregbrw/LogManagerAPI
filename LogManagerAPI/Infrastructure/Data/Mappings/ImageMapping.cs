namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ImageMapping : BaseMapping<Image>
{
    public override void Configure(EntityTypeBuilder<Image> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_image");

        builder.Property(i => i.FileGuid)
            .HasColumnName("file_guid")
            .HasColumnType("uuid")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(i => i.ImageContentS)
            .HasColumnName("image_content_small")
            .HasColumnType("bytea")
            .IsRequired();

        builder.Property(i => i.ImageContentM)
            .HasColumnName("image_content_medium")
            .HasColumnType("bytea")
            .IsRequired();

        builder.Property(i => i.ImageContentL)
            .HasColumnName("image_content_large")
            .HasColumnType("bytea")
            .IsRequired();

        builder.Property(i => i.Extension)
            .HasColumnName("extension")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasAlternateKey(a => a.FileGuid);

    }
}