namespace Application.Interfaces.Services.Core;

public interface ICsvService
{
    IEnumerable<T> ImportFromCsv<T>(Stream fileStream) where T : new();
    public Stream ExportToCsv<T>(IEnumerable<T> records, char delimiter = ';') where T : new();
}