namespace API.Middlewares;

using System.Text.Json;
using API.Resources;
using Application.Exceptions.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(context, ex, _logger);
        }
    }

    private static async Task HandleErrorAsync(HttpContext context, Exception exception, ILogger logger)
    {
        var localizer = context.RequestServices.GetRequiredService<IStringLocalizer<Errors>>();

        var problemDetails = exception switch
        {
            AppException e => new ProblemDetails
            {
                Status = e.Status,
                Title = e.Title,
                Detail = localizer[e.ResourceKey],
                Type = e.Type
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unknown Internal Server Error",
                Detail = localizer["UnknownError"],
                Type = "https://httpstatuses.com/500"
            }
        };

        var statusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        if (statusCode >= 500 && statusCode <= 599)
            logger.LogError(exception, "An unhandled exception occurred.");

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var jsonResponse = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(jsonResponse);
    }
}