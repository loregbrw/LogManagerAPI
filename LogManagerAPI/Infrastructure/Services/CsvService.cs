namespace Infrastructure.Services;

using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text;
using Application.Attributes;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services.Core;
using CsvHelper;
using CsvHelper.Configuration;

public class CsvService : ICsvService
{
    private static readonly ImmutableArray<char> _separators = [';', '|', '\t', ','];
    private static readonly Dictionary<Type, IAppConverter> _converters = [];

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

        var converters = propToHeader.Keys
            .ToDictionary(
                prop => prop,
                prop => prop.GetCustomAttribute<AppConverterAttribute>()
            );

        while (csv.Read())
        {
            var obj = new T();

            foreach (var (prop, header) in propToHeader)
            {
                if (converters.TryGetValue(prop, out var converterAttribute) && converterAttribute is not null)
                {
                    var value = csv.GetField(header);
                    var converter = GetOrCreateConverter(converterAttribute.ConverterType);
                    var convertedValue = converter.ConvertFromString(value);
                    prop.SetValue(obj, convertedValue);
                }
                else
                {
                    if (csv.TryGetField(prop.PropertyType, header, out var value))
                        prop.SetValue(obj, value);
                }
            }

            ApplyDataAnnotations(obj);
            yield return obj;
        }
    }

    public Stream ExportToCsv<T>(IEnumerable<T> records, char delimiter = ';') where T : new()
    {
        if (!_separators.Contains(delimiter)) throw new BadRequestException("InvalidDelimiter");

        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var headers = props.Select(p =>
        {
            var alias = p.GetCustomAttribute<AppAliasAttribute>()?.Alias;
            return string.IsNullOrWhiteSpace(alias) ? p.Name : alias;
        }).ToArray();

        var converters = props
            .ToDictionary(
                prop => prop,
                prop => prop.GetCustomAttribute<AppConverterAttribute>()
            );

        var ms = new MemoryStream();

        using (var writer = new StreamWriter(ms, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true), leaveOpen: true))
        {
            writer.WriteLine(string.Join(delimiter, headers.Select(h => FormatCsvField(h, delimiter))));

            foreach (var record in records)
            {
                var fields = new List<string>(props.Length);

                foreach (var prop in props)
                {
                    var value = prop.GetValue(record);

                    if (converters.TryGetValue(prop, out var converterAttribute) && converterAttribute is not null)
                    {
                        var converter = GetOrCreateConverter(converterAttribute.ConverterType);
                        fields.Add(FormatCsvField(converter.ConvertToString(value), delimiter));
                    }
                    else
                    {
                        fields.Add(FormatCsvField(value?.ToString(), delimiter));
                    }
                }

                writer.WriteLine(string.Join(delimiter, fields));
            }

            writer.Flush();
        }

        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    private static char DetectDelimiterFromStream(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream, leaveOpen: true);
        var headerLine = reader.ReadLine() ?? string.Empty;

        var best = _separators
            .Select(s => new { Separator = s, Count = headerLine.Count(c => c == s) })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        return best?.Separator ?? ',';
    }

    private static IAppConverter GetOrCreateConverter(Type converterType)
    {
        if (!_converters.TryGetValue(converterType, out var converter))
        {
            converter = (IAppConverter)Activator.CreateInstance(converterType)!;
            _converters[converterType] = converter;
        }
        return converter;
    }

    private static void ApplyDataAnnotations(object obj)
    {
        Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);
    }

    private static string FormatCsvField(string? field, char delimiter)
    {
        if (field is null) return string.Empty;

        if (field.Contains(delimiter) || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
        {
            var escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        return field;
    }
}