namespace Infrastructure.Services;

using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using Application.Attributes;
using Application.Exceptions;
using Application.Interfaces.Services.Core;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class CsvService : ICsvService
{
    private static readonly ImmutableArray<char> SeparatorChars = [';', '|', '\t', ','];


    public IEnumerable<T> ImportFromCsv<T>(Stream fileStream) where T : new()
    {
        if (fileStream.Length == 0)
            throw new BadRequestException("EmptyCsv");

        var delimiter = DetectDelimiterFromStream(fileStream);
        fileStream.Seek(0, SeekOrigin.Begin);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter.ToString(),
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            IgnoreBlankLines = true,
            TrimOptions = TrimOptions.Trim
        };

        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, config);

        RegisterTypeConverters<T>(csv);

        if (!csv.Read() || !csv.ReadHeader())
            throw new BadRequestException("MissingHeader");

        var headers = csv.HeaderRecord ?? [];
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var aliasToProp = properties
            .SelectMany(p =>
            {
                var aliases = p.GetCustomAttributes<AppAliasAttribute>()
                               .Select(a => a.Alias)
                               .Append(p.Name);
                return aliases.Select(alias => new { Alias = alias, Prop = p });
            })
            .ToDictionary(x => x.Alias, x => x.Prop, StringComparer.OrdinalIgnoreCase);


        var propToHeader = new Dictionary<PropertyInfo, string>();
        foreach (var header in headers)
        {
            if (!aliasToProp.TryGetValue(header, out var prop))
                continue;

            if (!propToHeader.TryAdd(prop, header))
                throw new BadRequestException("DuplicateHeaders", prop.Name);
        }

        if (propToHeader.Count == 0)
            throw new BadRequestException("NoMatchingHeaders");

        while (csv.Read())
        {
            var obj = new T();

            foreach (var (prop, header) in propToHeader)
            {
                if (csv.TryGetField(prop.PropertyType, header, out var value))
                    prop.SetValue(obj, value);
            }

            ApplyDataAnnotations(obj);
            yield return obj;
        }
    }

    private static char DetectDelimiterFromStream(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream, leaveOpen: true);
        var headerLine = reader.ReadLine() ?? string.Empty;

        var best = SeparatorChars
            .Select(s => new { Separator = s, Count = headerLine.Count(c => c == s) })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        return best?.Separator ?? ',';
    }

    private static void RegisterTypeConverters<T>(CsvReader csv)
    {
        var map = new DefaultClassMap<T>();

        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var memberMap = map.Map(typeof(T), prop);

            var converterAttr = prop.GetCustomAttribute<TypeConverterAttribute>();
            if (converterAttr != null)
            {
                var converterType = Type.GetType(converterAttr.ConverterTypeName)
                    ?? throw new InvalidOperationException($"Cannot find converter {converterAttr.ConverterTypeName}");
                var converter = (ITypeConverter)Activator.CreateInstance(converterType)!;
                memberMap.TypeConverter(converter);
            }
        }

        csv.Context.RegisterClassMap(map);
    }

    private static void ApplyDataAnnotations(object obj)
    {
        Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
    }
}