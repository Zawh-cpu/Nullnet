using Gateway.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Presentation.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(
    ILogger<ExceptionHandlingMiddleware> logger,
    IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails;

        switch (exception)
        {
            case ValidationException validationException:
                logger.LogWarning(
                    "Validation error. Title: {Title}. Message: {Message}. TraceId: {TraceId}",
                    validationException.Title,
                    validationException.Message,
                    httpContext.TraceIdentifier);

                problemDetails = new ProblemDetails
                {
                    Status = validationException.StatusCode,
                    Title = validationException.Title,
                    Type = validationException.Type,
                    Detail = validationException.Message,
                    Instance = httpContext.Request.Path
                };

                problemDetails.Extensions["errors"] = validationException.Errors;
                break;

            case AppException appException:
                logger.LogWarning(
                    "Application error. Title: {Title}. Message: {Message}. TraceId: {TraceId}",
                    appException.Title,
                    appException.Message,
                    httpContext.TraceIdentifier);

                problemDetails = new ProblemDetails
                {
                    Status = appException.StatusCode,
                    Title = appException.Title,
                    Type = appException.Type,
                    Detail = appException.Message,
                    Instance = httpContext.Request.Path
                };
                break;

            case UnauthorizedAccessException:
                logger.LogWarning(
                    "Unauthorized access. Path: {Path}. TraceId: {TraceId}",
                    httpContext.Request.Path,
                    httpContext.TraceIdentifier);

                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Type = "https://httpstatuses.com/401",
                    Detail = "Authentication is required.",
                    Instance = httpContext.Request.Path
                };
                break;

            default:
                logger.LogError(
                    exception,
                    "Unhandled exception. Method: {Method}. Path: {Path}. TraceId: {TraceId}",
                    httpContext.Request.Method,
                    httpContext.Request.Path,
                    httpContext.TraceIdentifier);

                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server error",
                    Type = "https://httpstatuses.com/500",
                    Detail = environment.IsDevelopment()
                        ? exception.ToString()
                        : "An unexpected error occurred.",
                    Instance = httpContext.Request.Path
                };
                break;
        }

        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}