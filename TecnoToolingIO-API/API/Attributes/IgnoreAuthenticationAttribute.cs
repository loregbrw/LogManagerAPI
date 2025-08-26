namespace API.Attributes;

/// <summary>
/// Indicates that the endpoint should bypass JWT authentication.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class IgnoreAuthenticationAttribute : Attribute;