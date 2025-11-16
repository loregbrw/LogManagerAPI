namespace Application.Attributes;

using Application.Interfaces;

[AttributeUsage(AttributeTargets.Property)]
public class AppConverterAttribute : Attribute
{
    public Type ConverterType { get; }

    public AppConverterAttribute(Type converterType)
    {
        if (!typeof(IAppConverter).IsAssignableFrom(converterType))
            throw new ArgumentException($"Type must implement IAppConverter", nameof(converterType));

        ConverterType = converterType;
    }
}