namespace Application.Models.Responses.Value;

public record GetValuesResponse(
    IEnumerable<ValueResponse> Values
);