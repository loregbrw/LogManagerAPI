using Application.Entities.Primitives;

namespace Application.Entities;

public class EmployeeDepartment : BaseEntity
{
    public required string Name { get; set; }
}