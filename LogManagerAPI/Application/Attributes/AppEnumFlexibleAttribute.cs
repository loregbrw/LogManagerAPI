namespace Application.Attributes;

using System;
using System.Linq;
using System.Reflection;
using Application.Exceptions;

[AttributeUsage(AttributeTargets.Property)]
public class AppEnumFlexibleAttribute<TEnum> : Attribute where TEnum : struct, Enum
{
    public object? Convert(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var value = text.Trim();

        if (Enum.TryParse<TEnum>(value, true, out var result))
            return result;

        if (int.TryParse(value, out var intVal) && Enum.IsDefined(typeof(TEnum), intVal))
            return (TEnum)Enum.ToObject(typeof(TEnum), intVal);

        foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var aliases = field.GetCustomAttributes<AppAliasAttribute>()
                               .Select(a => a.Alias);

            if (aliases.Any(a => string.Equals(a, value, StringComparison.OrdinalIgnoreCase)))
                return (TEnum)field.GetValue(null)!;
        }

        throw new BadRequestException("InvalidEnumValue", typeof(TEnum).Name);
    }
}