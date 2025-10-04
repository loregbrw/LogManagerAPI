namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StockDepartmentMapping : BaseMapping<StockDepartment>
{
    public override void Configure(EntityTypeBuilder<StockDepartment> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_stock_department");

        builder.Property(s => s.Name)
               .HasColumnName("name")
               .HasMaxLength(100);
    }
}