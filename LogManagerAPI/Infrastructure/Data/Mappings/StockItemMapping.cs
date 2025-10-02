namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StockItemMapping : BaseMapping<StockItem>
{
    public override void Configure(EntityTypeBuilder<StockItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_stock_item");

        builder.Property(s => s.Code)
               .HasColumnName("code")
               .HasMaxLength(50);

        builder.Property(s => s.Description)
               .HasColumnName("description")
               .HasMaxLength(500);

        builder.HasOne(s => s.UnitOfMeasurement)
               .WithMany()
               .HasForeignKey("unit_of_measurement_id")
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(s => s.Localization)
               .HasColumnName("localization")
               .HasMaxLength(255);

        builder.HasOne(s => s.StockDepartment)
               .WithMany()
               .HasForeignKey("stock_department_id")
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(s => s.StockGroup)
               .HasColumnName("stock_group")
               .HasConversion<short>()
               .HasColumnType("smallint");

        builder.HasOne(s => s.StockSubgroup)
               .WithMany()
               .HasForeignKey("stock_subgroup_id")
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(s => s.Cost)
               .HasColumnName("cost");

        builder.Property(s => s.MinimumStock)
               .HasColumnName("minimum_stock");

        builder.Property(s => s.Inbound)
               .HasColumnName("inbound");

        builder.Property(s => s.Outbound)
               .HasColumnName("outbound");

        builder.Property(s => s.Current)
               .HasColumnName("current");

        builder.Property(s => s.StockSituation)
               .HasColumnName("stock_situation")
               .HasConversion<short>()
               .HasColumnType("smallint");
    }
}