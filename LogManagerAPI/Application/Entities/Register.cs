namespace Application.Entities;

using Application.Entities.Primitives;
using Application.Enums;

public class Register : BaseEntity
{
    public required ERegisterType RegisterType { get; set; }
    public required StockItem StockItem { get; set; }
    public required double Amount { get; set; }
    public User? User { get; set; }
    public required DateOnly Date { get; set; }
    public string? Observation { get; set; }
}