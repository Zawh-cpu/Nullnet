namespace Gateway.Domain.Exceptions;

public sealed class RoleAssignmentNotFound : AppException
{
    public RoleAssignmentNotFound(Guid userId, Guid resourceId, Guid roleId)
        : base(
            message: $"RoleAssignment associated with user ({userId}), resource ({resourceId}), role ({roleId}) was not found.",
            statusCode: StatusCodes.Status404NotFound,
            title: "User not found",
            type: "https://httpstatuses.com/404")
    {
    }
}