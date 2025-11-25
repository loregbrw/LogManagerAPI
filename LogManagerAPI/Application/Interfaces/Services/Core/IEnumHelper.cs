namespace Application.Interfaces.Services.Core;

using Application.Models.Responses.Value;

public interface IEnumHelper
{
    IEnumerable<ValueResponse> GetEnumValuesResponse<TEnum>() where TEnum : Enum;
}