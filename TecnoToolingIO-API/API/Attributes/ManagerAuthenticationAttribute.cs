namespace API.Attributes;

/// <summary>
/// Indicates that the endpoint can only be accessed by users with the MANAGER role.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ManagerAuthenticationAttribute : Attribute;