
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