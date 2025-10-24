namespace Application.Interfaces.Services.Core;

public interface ICsvService
{
    IEnumerable<T> ImportFromCsv<T>(Stream fileStream) where T : new();
}