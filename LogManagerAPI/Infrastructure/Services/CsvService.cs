namespace Infrastructure.Services;

using System.Globalization;
using Application.Exceptions;
using Application.Interfaces.Services.Core;
using CsvHelper;
using CsvHelper.Configuration;

public class CsvService : ICsvService
{
    public IEnumerable<T> ImportFromCsv<T>(Stream fileStream) where T : new()
    {
        if (fileStream.Length == 0)
            throw new BadRequestException("EmptyCsv");

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            IgnoreBlankLines = true,
            TrimOptions = TrimOptions.Trim
        };

        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, config);

        csv.Read();
        csv.ReadHeader();

        var properties = typeof(T).GetProperties();

        while (csv.Read())
        {
            var obj = new T();
            foreach (var prop in properties)
            {
                if (csv.TryGetField(prop.PropertyType, prop.Name, out var value))
                    prop.SetValue(obj, value);
            }
            yield return obj;
        }
    }
}