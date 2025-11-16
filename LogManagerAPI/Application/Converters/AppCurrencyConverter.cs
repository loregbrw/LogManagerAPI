namespace Application.Converters;

using System.Globalization;
using Application.Exceptions;
using Application.Interfaces;

public class AppCurrencyConverter : IAppConverter
{
    public object? ConvertFromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var cleaned = value
            .Replace("R$", "", StringComparison.OrdinalIgnoreCase)
            .Replace(" ", "")
            .Replace(".", "")
            .Replace("-", "")
            .Replace(",", ".")
            .Trim();

        if (string.IsNullOrWhiteSpace(cleaned))
            return null;

        return decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result
            : throw new BadRequestException("InvalidCurrencyFormat", value);
    }

    public string? ConvertToString(object? value)
    {
        if (value is null) return null;

        var decimalValue = value switch
        {
            decimal d => d,
            int i => i,
            long l => l,
            double db => (decimal)db,
            float f => (decimal)f,
            _ => throw new BadRequestException("InvalidCurrencyType", value.GetType().Name)
        };

        return decimalValue.ToString("C2", new CultureInfo("pt-BR"));
    }
}