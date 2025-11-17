namespace Application.Models.Responses.Enum;

public record GetValuesResponse(
    IEnumerable<ValueResponse> Values
);