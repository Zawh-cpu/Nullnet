namespace Gateway.Domain.Exceptions;

public sealed class ValidationException : AppException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base(
            message: "One or more validation errors occurred.",
            statusCode: StatusCodes.Status400BadRequest,
            title: "Validation error",
            type: "https://httpstatuses.com/400")
    {
        Errors = errors;
    }
}