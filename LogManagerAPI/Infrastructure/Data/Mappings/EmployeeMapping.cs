namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
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

        builder.HasOne(e => e.ProfileImage)
                .WithMany()
                .HasForeignKey("profile_image_id")
                .HasPrincipalKey(i => i.Id)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

        builder.HasOne(e => e.EmployeeDepartment)
                .WithMany()
                .HasForeignKey("employee_department_id")
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
    }
}