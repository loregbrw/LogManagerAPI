namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UnitOfMeasurementMapping : BaseMapping<UnitOfMeasurement>
{
    public override void Configure(EntityTypeBuilder<UnitOfMeasurement> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_unit_of_measurement");

        builder.Property(u => u.Name)
               .HasColumnName("name")
               .HasMaxLength(10);
    }
}