namespace Infrastructure.Mappings;

using Application.Entities;
using Infrastructure.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeMapping : BaseMapping<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_employee");

        builder.Property(e => e.Code)
               .HasColumnName("code")
               .HasColumnType("smallint");

        builder.Property(e => e.Name)
               .HasColumnName("name")
               .HasMaxLength(255);

        builder.Property(e => e.Email)
               .HasColumnName("email")
               .HasMaxLength(255);

        builder.Property(e => e.Password)
               .HasColumnName("password")
               .HasMaxLength(255);

        builder.Property(e => e.Role)
               .HasColumnName("role")
               .HasConversion<short>()
               .HasColumnType("smallint");
    }
}