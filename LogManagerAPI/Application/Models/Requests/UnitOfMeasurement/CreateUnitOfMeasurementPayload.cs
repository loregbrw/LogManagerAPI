namespace Application.Models.Requests.UnitOfMeasurement;

using System.ComponentModel.DataAnnotations;

public class CreateUnitOfMeasurementPayload
{
    [StringLength(10)]
    public required string Name { get; set; }
}