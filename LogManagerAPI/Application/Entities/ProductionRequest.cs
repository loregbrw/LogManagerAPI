using Application.Models.Options;

namespace Application.Entities;

public class ProductionRequest
{
    public required string Code { get; set; }
    public Client? Client { get; set; }
    public string? Description { get; set; }
    public required User User { get; set; }
    public DateOnly? SolicitationDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public string? Observation { get; set; }
    public required EProductionRequestStatus ProductionRequestStatus { get; set; }
}