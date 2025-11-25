namespace Application.Models.Requests.StockDepartment;

using System.ComponentModel.DataAnnotations;

public class CreateStockDepartmentPayload
{
    [StringLength(100)]
    public required string Name { get; set; }
}