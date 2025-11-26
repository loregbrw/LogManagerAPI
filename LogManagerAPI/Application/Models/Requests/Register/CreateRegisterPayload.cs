namespace Application.Models.Requests.Register;

using System.ComponentModel.DataAnnotations;
using Application.Enums;

public class CreateRegisterPayload
{
    public required Guid StockItemId { get; set; }
    public required double Amount { get; set; }

    [EnumDataType(typeof(ERegisterType))]
    public required ERegisterType RegisterType { get; set; }

    [StringLength(500)]
    public string? Observation { get; set; }
}