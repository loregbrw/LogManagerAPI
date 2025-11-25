namespace Infrastructure.Services;

using System.Reflection;
using Application.Attributes;
using Application.Interfaces.Services.Core;
using Application.Models.Responses.Value;

public class EnumHelper : IEnumHelper
{
    public IEnumerable<ValueResponse> GetEnumValuesResponse<TEnum>() where TEnum : Enum
    {
        var type = typeof(TEnum);

        var result = Enum.GetValues(type)
            .Cast<TEnum>()
            .Select(e =>
            {
                var value = e.ToString()!;
                var member = type.GetMember(value).First();

                var aliasAttribute = member.GetCustomAttribute<AppAliasAttribute>();

                string label = aliasAttribute?.Alias ?? value;

                return new ValueResponse(value, label);
            })
            .ToList();

        return result;
    }
}