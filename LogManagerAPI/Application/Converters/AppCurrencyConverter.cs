namespace Application.Converters;

using System;
using System.Globalization;
using Application.Exceptions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class AppCurrencyConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var cleaned = text
            .Replace("R$", "", StringComparison.OrdinalIgnoreCase)
            .Replace(" ", "")
            .Replace(".", "")
            .Replace(",", ".")
            .Trim();

        return decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var value)
            ? value
            : throw new BadRequestException("InvalidCurrencyFormat", text);
    }
}
