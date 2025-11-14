namespace Application.Converters;

using System;
using System.Linq;
using System.Reflection;
using Application.Attributes;
using Application.Exceptions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class AppEnumFlexibleConverter<TEnum> : DefaultTypeConverter where TEnum : struct, Enum
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
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
                               .Select(a => a.Alias)
                               .ToArray();

            if (aliases.Any(a => string.Equals(a, value, StringComparison.OrdinalIgnoreCase)))
                return (TEnum)field.GetValue(null)!;
        }

        throw new BadRequestException("InvalidEnumValue", typeof(TEnum).Name, text);
    }
}