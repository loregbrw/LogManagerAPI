namespace Application.Models.Requests.StockSubgroup;

using System.ComponentModel.DataAnnotations;

public class CreateStockSubgroupPayload
{
    [StringLength(255)]
    public required string Name { get; set; }
}