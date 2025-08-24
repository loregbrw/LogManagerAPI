namespace API.Attributes;

/// <summary>
/// Indicates that the endpoint can only be accessed by users with the ADMIN role.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AdminAuthenticationAttribute : Attribute;