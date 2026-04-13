namespace Database.Domain.Exceptions;

public sealed class UserNotFoundException : AppException
{
    public UserNotFoundException(Guid userId)
        : base(
            message: $"User with id '{userId}' was not found.",
            statusCode: StatusCodes.Status404NotFound,
            title: "User not found",
            type: "https://httpstatuses.com/404")
    {
    }
}