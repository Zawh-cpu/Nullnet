namespace Database.Domain.Exceptions;

public sealed class ServerNotFoundException : AppException
{
    public ServerNotFoundException(Guid userId)
        : base(
            message: $"Server with id '{userId}' was not found.",
            statusCode: StatusCodes.Status404NotFound,
            title: "Server not found",
            type: "https://httpstatuses.com/404")
    {
    }
}