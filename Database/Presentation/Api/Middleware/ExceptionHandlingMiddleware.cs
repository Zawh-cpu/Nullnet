using Database.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(
    ILogger<ExceptionHandlingMiddleware> logger,
    IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Unhandled exception. TraceId: {TraceId}",
            httpContext.TraceIdentifier);

        ProblemDetails problemDetails;

        switch (exception)
        {
            case ValidationException validationException:
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
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server error",
                    Type = "https://httpstatuses.com/500",
                    Detail = environment.IsDevelopment()
                        ? exception.Message
                        : "An unexpected error occurred.",
                    Instance = httpContext.Request.Path
                };
                break;
        }

        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}