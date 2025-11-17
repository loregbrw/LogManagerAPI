namespace Application.Interfaces.Services.Core;

using Application.Models.Responses.Enum;

public interface IEnumHelper
{
    IEnumerable<ValueResponse> GetEnumValuesResponse<TEnum>() where TEnum : Enum;
}