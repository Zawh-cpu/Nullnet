namespace Database.Domain.Exceptions;

public sealed class RoleNotFoundException : AppException
{
    public RoleNotFoundException(Guid roleId)
        : base(
            message: $"Role with id '{roleId}' was not found.",
            statusCode: StatusCodes.Status404NotFound,
            title: "Role not found",
            type: "https://httpstatuses.com/404")
    {
    }
}