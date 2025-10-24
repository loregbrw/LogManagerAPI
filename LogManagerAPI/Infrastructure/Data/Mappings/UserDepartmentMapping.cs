namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserDepartmentMapping : BaseMapping<UserDepartment>
{
    public override void Configure(EntityTypeBuilder<UserDepartment> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_user_department");

        builder.Property(u => u.Name)
               .HasColumnName("name")
               .HasMaxLength(255);
    }
}