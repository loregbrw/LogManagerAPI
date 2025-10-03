namespace Application.Exceptions.Primitives;

using System.Net;

public abstract class AppException : Exception
{
    public int Status { get; }

    public string Title { get; }

    public string Type { get; }

    public string ResourceKey { get; }

    public object[]? Args { get; }

   
    public AppException(string resourceKey, HttpStatusCode statusCode, object[]? args = null, string? type = null)
        : base(resourceKey)
    {
        ResourceKey = resourceKey;
        Status = (int)statusCode;
        Title = statusCode.ToString();
        Args = args;
        Type = type ?? $"https://httpstatuses.com/{(int)statusCode}";
    }

    
    public AppException(string resourceKey, HttpStatusCode statusCode, Exception inner, object[]? args = null, string? type = null)
        : base(resourceKey, inner)
    {
        ResourceKey = resourceKey;
        Status = (int)statusCode;
        Title = statusCode.ToString();
        Args = args;
        Type = type ?? $"https://httpstatuses.com/{(int)statusCode}";
    }
}