namespace Application.Interfaces;

public interface IAppConverter
{
    object? ConvertFromString(string? value);
}