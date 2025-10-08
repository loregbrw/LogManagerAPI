namespace Infrastructure.Data.Mappings;

using Application.Entities;
using Infrastructure.Data.Mappings.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMapping : BaseMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("tb_user");

        builder.Property(u => u.Code)
               .HasColumnName("code")
               .HasColumnType("smallint");

        builder.Property(u => u.Name)
               .HasColumnName("name")
               .HasMaxLength(255);

        builder.Property(u => u.Email)
               .HasColumnName("email")
               .HasMaxLength(255);

        builder.Property(u => u.Password)
               .HasColumnName("password")
               .HasMaxLength(255);

        builder.Property(u => u.Role)
               .IsRequired()
               .HasColumnName("role")
               .HasConversion<short>()
               .HasColumnType("smallint");

        builder.HasOne(u => u.ProfileImage)
                .WithMany()
                .HasForeignKey("profile_image_id")
                .HasPrincipalKey(i => i.Id)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

        // builder.HasOne(e => e.UserDepartment)
        //         .WithMany()
        //         .HasForeignKey("User_department_id")
        //         .HasPrincipalKey(e => e.Id)
        //         .OnDelete(DeleteBehavior.SetNull)
        //         .IsRequired(false);
    }
}