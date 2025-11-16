namespace Application.Converters;

using System.Linq;
using System.Reflection;
using Application.Attributes;
using Application.Exceptions;
using Application.Interfaces;

public class AppEnumFlexibleConverter<TEnum> : IAppConverter where TEnum : struct, Enum
{
    public object? ConvertFromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var text = value.Trim();

        if (Enum.TryParse<TEnum>(text, true, out var result))
            return result;

        if (int.TryParse(text, out var intVal) && Enum.IsDefined(typeof(TEnum), intVal))
            return (TEnum)Enum.ToObject(typeof(TEnum), intVal);

        foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var aliases = field.GetCustomAttributes<AppAliasAttribute>()
                               .Select(a => a.Alias)
                               .ToArray();

            if (aliases.Any(a => string.Equals(a, text, StringComparison.OrdinalIgnoreCase)))
                return (TEnum)field.GetValue(null)!;
        }

        throw new BadRequestException("InvalidEnumValue", typeof(TEnum).Name, value);
    }

    public string? ConvertToString(object? value)
    {
        if (value is null) return null;

        var enumValue = (TEnum)value;
        var field = typeof(TEnum).GetField(enumValue.ToString());

        if (field is null) return enumValue.ToString();

        return field.GetCustomAttribute<AppAliasAttribute>()?.Alias ?? enumValue.ToString();
    }
}