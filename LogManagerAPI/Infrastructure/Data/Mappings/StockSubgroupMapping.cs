namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StockSubgroupMapping : BaseMapping<StockSubgroup>
{
    public override void Configure(EntityTypeBuilder<StockSubgroup> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_stock_subgroup");

        builder.Property(s => s.Name)
               .HasColumnName("name")
               .HasMaxLength(255);
    }
}