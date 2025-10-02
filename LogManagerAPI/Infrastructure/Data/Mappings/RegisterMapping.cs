namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RegisterMapping : BaseMapping<Register>
{
    public override void Configure(EntityTypeBuilder<Register> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_register");

        builder.Property(s => s.RegisterType)
               .HasColumnName("register_type")
               .HasConversion<short>()
               .HasColumnType("smallint");

        builder.HasOne(u => u.StockItem)
                .WithMany()
                .HasForeignKey("stock_item_id");

        builder.Property(u => u.Amount)
               .HasColumnName("amount");

        builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey("user_id")
                .OnDelete(DeleteBehavior.SetNull);

        builder.Property(u => u.Date)
               .HasColumnName("date");

        builder.Property(u => u.Observation)
               .HasColumnName("observation")
               .HasMaxLength(500);
    }
}