
/*
    LogManager API
 - Inventory Management Software with incoming and outgoing stock control.
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

namespace Application.Models.Requests.Employee;

using System.ComponentModel.DataAnnotations;
using Application.Enums;

public class CreateEmployee
{
    public required short Code { get; set; }

    [StringLength(255)]
    public required string Name { get; set; }

    [StringLength(255)]
    public required string Email { get; set; }

    [StringLength(50)]
    public required string Password { get; set; }

    [EnumDataType(typeof(ERole))]
    public required ERole Role { get; set; }
}