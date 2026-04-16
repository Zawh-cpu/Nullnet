namespace Gateway.Domain.Exceptions;

public sealed class ForbiddenException : AppException
{
    public ForbiddenException(string message = "You do not have access to this resource.")
        : base(
            message: message,
            statusCode: StatusCodes.Status403Forbidden,
            title: "Forbidden",
            type: "https://httpstatuses.com/403")
    {
    }
}
