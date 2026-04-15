namespace Database.Application.DTO.Entities;

public class UserDto(Guid id, string userName, Guid resourceId, bool isVerified, bool isActive, DateTime createdAt)
{
    public Guid Id { get; } = id;
    public string UserName { get; } = userName;
    public Guid ResourceId { get; set; } = resourceId;
    public bool IsVerified { get; set; } = isVerified;
    public bool IsActive { get; set; } = isActive;
    public DateTime CreatedAt { get; set; } = createdAt;
}