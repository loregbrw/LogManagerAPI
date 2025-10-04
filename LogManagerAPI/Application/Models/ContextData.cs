namespace Application.Models;

using Application.Enums;

public readonly record struct ContextData(Guid UserId, ERole UserRole);