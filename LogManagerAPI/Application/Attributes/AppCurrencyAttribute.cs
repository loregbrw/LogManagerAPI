namespace Application.Attributes;

using System;
using System.Globalization;
using Application.Exceptions;

[AttributeUsage(AttributeTargets.Property)]
public class AppCurrencyAttribute : Attribute
{
    public object? Convert(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var cleaned = text
            .Replace("R$", "", StringComparison.OrdinalIgnoreCase)
            .Replace(" ", "")
            .Replace(".", "")
            .Replace(",", ".")
            .Trim();

        return decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var val)
            ? val
            : throw new BadRequestException("InvalidCurrencyFormat", text);
    }
}
