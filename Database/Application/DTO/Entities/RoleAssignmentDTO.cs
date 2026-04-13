namespace Database.Application.DTO.Entities;

public sealed class RoleAssignmentDto
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid ResourceId { get; }
    public Guid RoleId { get; }
    
    public RoleAssignmentDto(Guid id, Guid userId, Guid resourceId, Guid roleId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Assignment id cannot be empty.", nameof(id));

        if (userId == Guid.Empty)
            throw new ArgumentException("User id cannot be empty.", nameof(userId));

        if (resourceId == Guid.Empty)
            throw new ArgumentException("Resource id cannot be empty.", nameof(resourceId));

        if (roleId == Guid.Empty)
            throw new ArgumentException("Role id cannot be empty.", nameof(roleId));

        Id = id;
        UserId = userId;
        ResourceId = resourceId;
        RoleId = roleId;
    }
}