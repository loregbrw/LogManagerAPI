namespace Application.Interfaces;

public interface IAppConverter
{
    object? ConvertFromString(string? value);
    string? ConvertToString(object? value);
}