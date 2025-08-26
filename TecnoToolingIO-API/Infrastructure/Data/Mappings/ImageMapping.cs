
/*
    TecnoToolingIO API - Inventory Management Software with incoming and outgoing stock control.
    Copyright (C) 2025 Lorena Gobara Falci

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contact: loregobara@gmail.com
*/

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