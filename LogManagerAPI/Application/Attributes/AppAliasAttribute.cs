namespace Application.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
public class AppAliasAttribute(string alias) : Attribute
{
    public string Alias { get; } = alias.ToLowerInvariant();
}